using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Windows.Forms;

namespace OutlookTools
{
    [ComVisible(true)]
    public class Outlook2RemedyRibbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public Outlook2RemedyRibbon()
        {
            //nothing to do here
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("OutlookTools.Outlook2RemedyRibbon.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit http://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        public System.Drawing.Bitmap GetImage(Office.IRibbonControl control)
        {
            switch (control.Id)
            {
                case "convertButton":
                    {
                        //add a icon for the button here
                        return new System.Drawing.Bitmap(AppDomain.CurrentDomain.BaseDirectory + @"\res\convert.png");
                    };
                case "configButton":
                    {
                        //add a icon for the button here
                        return new System.Drawing.Bitmap(AppDomain.CurrentDomain.BaseDirectory + @"\res\settings.png");
                    };
            }
            return null;
        }

        #endregion


        #region "Button: Convert Settings"

        public void OnConfigButton(Office.IRibbonControl control)
        {
            Outlook2RemedyConfig config = new Outlook2RemedyConfig();
            config.ShowDialog();
        }

        #endregion


        #region "Button: Convert Email2Ticket"

		public void OnConvertButton(Office.IRibbonControl control)
		{
			StagingTicket ticket = new StagingTicket();

			Outlook.Application application = new Outlook.Application();
			Outlook.NameSpace ns = application.GetNamespace("MAPI");

            //get selected outlook object / mail item
            Object selectedObject = application.ActiveExplorer().Selection[1];
            Outlook.MailItem selectedMail = (Outlook.MailItem)selectedObject;

			//in case a email object si selected run the workflow
			if (selectedObject != null)
			{
				//1. invoke web service
				try
				{
					//instanciate service and invoke it create the ticket and to receive the ticket reference (ID)
					Remedy2OutlookService service = new Remedy2OutlookService();
					ticket = service.invoke(selectedMail);
				}
				catch(Exception ex)
				{
					string path = AppDomain.CurrentDomain.BaseDirectory + @"\temp";
					if (!Directory.Exists(path)) Directory.CreateDirectory(path);

					using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + @"\errors.log", true))
					{
						file.WriteLine(DateTime.Now.ToString() + " - " + ex.Message + "\n" + ex.ToString());
					}

					// display an error message
					MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}


				//2. open submitted ticket in browser or display info/warn/error messages
				if (Properties.Settings.Default.OpenInBrowser && ticket.wid != null)
				{
					dynamic ie = Activator.CreateInstance(Type.GetTypeFromProgID("InternetExplorer.Application"));
					string url = Properties.Settings.Default.AppConsoleURL;

					if(url.EndsWith("/")) url.Remove(url.Length - 1);
					if (url.IndexOf("?", 0) > 0) url += "&eid=" + ticket.wid;
					else url += "?eid=" + ticket.wid;

					ie.AddressBar = false;
					ie.MenuBar = false;
					ie.ToolBar = false;
					ie.Visible = true;
					ie.Navigate2(url);
				}
				else
				{
					if (ticket.wid != null && String.Equals(ticket.sts, "done")) MessageBox.Show("Remedy workflow ticket has been created: " + ticket.wid, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					else if (ticket.rid != null && String.Equals(ticket.sts, "open")) MessageBox.Show("Selected email is transferred to Remedy into the staging record [" + ticket.rid + "] but the fulfillment ticket was not created due to an error or misconfiguration.\n\nContact your Administrator!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					else if (String.Equals(ticket.sts, "error") && ticket.log != null) MessageBox.Show("Remedy workflow error: " + ticket.log + ".\n\nContact your Administrator!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}


				//3. mark selected email as read
				if (Properties.Settings.Default.ReadMail && ticket.wid != null)
				{
					selectedMail.UnRead = false;
					selectedMail.Save();
				}


				//4. insert reference ticket number in the 
				if (Properties.Settings.Default.InsertInSubject && ticket.wid != null)
				{
					selectedMail.Subject = ticket.wid + ": " + selectedMail.Subject;
					selectedMail.Save();
				}

				//5. copy selected email item into Backup MAPI folder (if not exist will be created)
				if (Properties.Settings.Default.BackupMail && ticket.wid != null)
				{
					Outlook.Folder inbox = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox) as Outlook.Folder;
					Outlook.MAPIFolder backup = null;

					foreach (Outlook.MAPIFolder subfolder in inbox.Folders)
					{
						if (String.Equals(subfolder.Name, "Backup"))
						{
							backup = subfolder;
							break;
						}
					}

					if(backup == null)
					{
						try
						{
							backup = inbox.Folders.Add("Backup", Outlook.OlDefaultFolders.olFolderInbox);
						}
						catch (Exception ex)
						{
							MessageBox.Show("Error trying to create Backup MAPI folder: " + ex.Message + ".\n\nContact your Administrator or create it manually!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}

					if(backup != null)
					{
						Outlook.MailItem copyMail = selectedMail.Copy();
						copyMail.Move(backup);
					}

					if (inbox != null) Marshal.ReleaseComObject(inbox);
					if (backup != null) Marshal.ReleaseComObject(backup);
				}

				//6. delete selected email item
				if (Properties.Settings.Default.RemoveMail && ticket.wid != null)
				{
					selectedMail.Delete();
				}
			}
		}

        #endregion

        #region "Get SMTP account methods "

        public Outlook.Account GetAccountForEmailAddress(Outlook.Application application, string smtpAddress)
        {
            // Loop over the Accounts collection of the current Outlook session.
            Outlook.Accounts accounts = application.Session.Accounts;

            foreach (Outlook.Account account in accounts)
            {
                // When the email address matches, return the account.
                if (account.SmtpAddress.ToLower() == smtpAddress.ToLower())
                {
                    return account;
                }
            }

            // If you get here, no matching account was found.
            throw new System.Exception(string.Format("No Account with SmtpAddress: {0} exists!", smtpAddress));
        }

        public Outlook.Account GetDefaultAccount(Outlook.Application application)
        {

            // Get the Store for CurrentFolder.
            Outlook.Folder folder = application.ActiveExplorer().CurrentFolder as Outlook.Folder;
            Outlook.Store store = folder.Store;
            Outlook.Accounts accounts = application.Session.Accounts;

            // Enumerate accounts to find
            // account.DeliveryStore for store.
            foreach (Outlook.Account account in accounts)
            {
                if (account.DeliveryStore.StoreID ==
                    store.StoreID)
                {
                    return account;
                }
            }

            // If you get here, no matching account was found.
            throw new System.Exception(string.Format("No Account found!"));
        }

#endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
