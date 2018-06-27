using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OutlookTools
{
    public partial class Outlook2RemedyConfig : Form
    {
        public Outlook2RemedyConfig()
        {
            InitializeComponent();
        }

        private void onSaveClick(object sender, EventArgs e)
        {
            Properties.Settings.Default.EventCode = ebEventCode.Text;
            Properties.Settings.Default.WebServiceURL = ebWebServiceURL.Text;
			Properties.Settings.Default.ReadMail = cbReadMail.Checked;
            Properties.Settings.Default.RemoveMail = cbRemoveMail.Checked;
			Properties.Settings.Default.BackupMail = cbBackupMail.Checked;
			Properties.Settings.Default.InsertInSubject = cbInsertInSubject.Checked;
            Properties.Settings.Default.OpenInBrowser = cbOpenInBrowser.Checked;
            Properties.Settings.Default.AppConsoleURL = ebAppConsoleURL.Text;
			Properties.Settings.Default.RemedyUsername = ebUsername.Text;
			Properties.Settings.Default.RemedyPassword = ebPassword.Text;
			Properties.Settings.Default.EmailAttachment = cbEmailAttachment.Checked;

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();

            ebEventCode.Text = Properties.Settings.Default.EventCode;
            ebWebServiceURL.Text = Properties.Settings.Default.WebServiceURL;
			cbReadMail.Checked = Properties.Settings.Default.ReadMail;
            cbRemoveMail.Checked = Properties.Settings.Default.RemoveMail;
			cbBackupMail.Checked = Properties.Settings.Default.BackupMail;
			cbInsertInSubject.Checked = Properties.Settings.Default.InsertInSubject;
            cbOpenInBrowser.Checked = Properties.Settings.Default.OpenInBrowser;
            ebAppConsoleURL.Text = Properties.Settings.Default.AppConsoleURL;
			ebUsername.Text = Properties.Settings.Default.RemedyUsername;
			ebPassword.Text = Properties.Settings.Default.RemedyPassword;
			cbEmailAttachment.Checked = Properties.Settings.Default.EmailAttachment;

			ebAppConsoleURL.Enabled = cbOpenInBrowser.Checked;
        }

		private void cbOpenInBrowser_CheckedChanged(object sender, EventArgs e)
		{
			ebAppConsoleURL.Enabled = cbOpenInBrowser.Checked;
		}
    }
}
