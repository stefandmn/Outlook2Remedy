using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;  
using System.Net;  
using System.Xml;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace OutlookTools
{
	/// <summary>
	/// Dedicated structure to keep the answer returned by web service. The answer is 
	/// translated in two ticket IDs, the first one indicate the ID of staging record 
	/// from stating form (Remedy2Outlook form) and the other one (wid) is the ticket 
	/// Id of the filfillment process that is triggered based on the conversion code
	/// sent by the Outlook AddIn.
	/// </summary>
	public struct StagingTicket
	{
		public string rid;
		public string wid;
		public string sts;
		public string log;
	}

    class Remedy2OutlookService
    {
		private string GetTicketReference(string subject)
		{
			string reft = "";

			if(subject != null)
			{
				foreach (string word in subject.Split(' '))
				{
					if (word.StartsWith("0000") ||
						word.StartsWith("REQ0") ||
						word.StartsWith("INC0") ||
						word.StartsWith("PBI0") ||
						word.StartsWith("CHG0") ||
						word.StartsWith("TAS0") ||
						word.StartsWith("WO00"))
					{
						reft = HandleSpecialChars(word.Trim());
						break;
					}
				}
			}

			return reft;
		}

		private string HandleSpecialChars(string str, string repl="")
		{
			// Create  a string array and add the special characters you want to remove
			string[] chars = new string[] { " ", ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "=", "+", "-" };

			//Iterate the number of times based on the String array length.
			for (int i = 0; i < chars.Length; i++)
			{
				if (!repl.Equals(chars[i]) && str.Contains(chars[i]))
				{
					str = str.Replace(chars[i], repl);
				}
			}

			return str;
		}

		public int GetEmailLength(string path)
		{
			FileStream objfilestream = new FileStream(path, FileMode.Open, FileAccess.Read);
			int len = (int)objfilestream.Length;
			objfilestream.Close();

			return len;
		}

		public Byte[] GetEmailContent(string path)
		{
			FileStream objfilestream = new FileStream(path, FileMode.Open, FileAccess.Read);
			int len = (int)objfilestream.Length;
			Byte[] doc = new Byte[len];
			objfilestream.Read(doc, 0, len);
			objfilestream.Close();

			return doc;
		}

		public StagingTicket invoke(Outlook.MailItem mail)
		{
			StagingTicket ticket = new StagingTicket();
			  
			//Calling CreateSOAPWebRequest method
			HttpWebRequest request = CreateSOAPWebRequest();
			XmlDocument SOAPReqBody = new XmlDocument();

			//declare SOAP message builder
			StringBuilder soap = new StringBuilder();
			//create envelope
			soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
			"<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:Outlook2Remedy\">\n");
			//create auth header
			soap.Append("\t<soapenv:Header>\n" +
			"\t\t<urn:AuthenticationInfo>\n" +
			"\t\t\t<urn:userName>" + Properties.Settings.Default.RemedyUsername + "</urn:userName>\n" +
			"\t\t\t<urn:password>" + Properties.Settings.Default.RemedyPassword + "</urn:password>\n" +
			"\t\t</urn:AuthenticationInfo>\n" +
			"\t</soapenv:Header>\n");
			//open Body and start creating event
			soap.Append("\t<soapenv:Body>\n" +
			"\t\t<urn:Create>\n");
			//add workflow variables
			soap.Append("\t\t\t<urn:Submitter>" + Environment.UserName.ToLower() + "</urn:Submitter>\n" +
			"\t\t\t<urn:EventCode>" + Properties.Settings.Default.EventCode + "</urn:EventCode>\n");
			//add email content (subject and body)
			soap.Append("\t\t\t<urn:EmailSubject>" + (mail.Subject != null ? WebUtility.HtmlEncode(mail.Subject) : "(no subject)") + "</urn:EmailSubject>\n" +
			"\t\t\t<urn:EmailBody>" + WebUtility.HtmlEncode(mail.Body) + "</urn:EmailBody>\n");
			//add attachment details
			if(Properties.Settings.Default.EmailAttachment)
			{
				string path = AppDomain.CurrentDomain.BaseDirectory + @"\temp";
				if (!Directory.Exists(path)) Directory.CreateDirectory(path);
				if (mail.Subject.Length > 50) path += @"\"+ HandleSpecialChars(mail.Subject.Substring(0, 49), "_") + ".msg";
					else path += @"\" + HandleSpecialChars(mail.Subject, "_") + ".msg";
				mail.SaveAs(path, Outlook.OlSaveAsType.olMSG);
				soap.Append("<urn:EML_attachmentName>" + path + "</urn:EML_attachmentName>");
				soap.Append("<urn:EML_attachmentData>" + Convert.ToBase64String(GetEmailContent(path)) + "</urn:EML_attachmentData>");
				soap.Append("<urn:EML_attachmentOrigSize>" + GetEmailLength(path) + "</urn:EML_attachmentOrigSize>");
				File.Delete(path);
			}
			//add releated ticket - if is found
			soap.Append("\t\t\t<urn:RelatedTicketID>" + GetTicketReference(mail.Subject) + "</urn:RelatedTicketID>\n");
			//add recepients
			soap.Append("\t\t\t<urn:EmailFrom>" + WebUtility.HtmlEncode(mail.SenderEmailAddress) + "</urn:EmailFrom>\n" +
			"\t\t\t<urn:EmailTO>" + WebUtility.HtmlEncode(mail.To) + "</urn:EmailTO>\n" +
			"\t\t\t<urn:EmailCC>" + WebUtility.HtmlEncode(mail.CC) + "</urn:EmailCC>\n");
			//closing event, Body and envelope
			soap.Append("\t\t</urn:Create>\n" +
			"\t</soapenv:Body>\n" +
			"</soapenv:Envelope>");

			//prepare XML SOAP request
			SOAPReqBody.LoadXml(soap.ToString());

			//send XML SOAP request to server
			using (Stream stream = request.GetRequestStream())  
			{  
				SOAPReqBody.Save(stream);
			}

			//Geting response from request  
			using (WebResponse Serviceres = request.GetResponse())  
			{  
				using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))  
				{
					//reading stream  
					var strResponse = rd.ReadToEnd();

					XmlDocument xmldoc = new XmlDocument();
					xmldoc.LoadXml(strResponse);
					XmlNamespaceManager nsm = new XmlNamespaceManager(xmldoc.NameTable);
					nsm.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
					nsm.AddNamespace("ns0", "urn:Outlook2Remedy");
					
					XmlNode node1 = xmldoc.SelectSingleNode("//soapenv:Envelope/soapenv:Body/ns0:CreateResponse/ns0:RID", nsm);
					if (node1 != null && node1.InnerText != null) ticket.rid = node1.InnerText;
						else ticket.rid = null;

					XmlNode node2 = xmldoc.SelectSingleNode("//soapenv:Envelope/soapenv:Body/ns0:CreateResponse/ns0:WID", nsm);
					if (node2 != null && node2.InnerText != null) ticket.wid = node2.InnerText;
						else ticket.wid = null;

					XmlNode node3 = xmldoc.SelectSingleNode("//soapenv:Envelope/soapenv:Body/ns0:CreateResponse/ns0:STS", nsm);
					if (node3 != null && node3.InnerText != null) ticket.sts = node3.InnerText;
						else ticket.sts = "open";

					XmlNode node4 = xmldoc.SelectSingleNode("//soapenv:Envelope/soapenv:Body/ns0:CreateResponse/ns0:LOG", nsm);
					if (node4 != null && node4.InnerText != null) ticket.log = node4.InnerText;
						else ticket.log = null;
				}  
			}

			return ticket;  
		} 
  
		public HttpWebRequest CreateSOAPWebRequest()  
		{  
			//Making Web Request  
			HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.WebServiceURL);

			//SOAPAction
			Req.Headers.Add("SOAPAction", "urn:Outlook2Remedy/Create");
			//Content_type  
			Req.ContentType = "text/xml;charset=\"utf-8\"";
			Req.Accept = "text/xml";
			//HTTP method  
			Req.Method = "POST";
  
			return Req;
		}
	}
}
