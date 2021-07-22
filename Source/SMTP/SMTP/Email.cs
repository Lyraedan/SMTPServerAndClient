/*
 
    Author: Zel (Luke Rapkin)
    Student ID: 100527733
    Created: 01/11/2020
    Last Modified: 31/12/2021

 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;

namespace SMTP
{
    class Email : MailMessage
    {
        MailAddress from = null;
        MailAddressCollection to = null;
        MailAddressCollection cc = null;

        string subject = string.Empty;
        string body = string.Empty;

        public Email() { }

        public new void From(string from)
        {
            base.From = new MailAddress(from);
            this.from = base.From;
        }

        public new void To(params string[] to)
        {
            if(to.Length <= 0)
            {
                MessageBox.Show("\"To\" recipients can not be <= 0");
                throw new System.Exception("\"To\" recipients can not be <= 0");
            }
            base.To.Clear();
            foreach(string recipient in to)
            {
                base.To.Add(new MailAddress(recipient));
            }
            this.to = base.To;
        }

        public new void CC(params string[] ccs)
        {
            if(ccs.Length <= 1) return;

            base.CC.Clear();
            foreach(string cc in ccs)
            {
                base.CC.Add(cc);
            }

            this.cc = base.CC;
        }

        public void Send(string subject, string body, bool wasForwardedForcefully = false)
        {
            this.subject = subject;
            this.body = body;
            base.Subject = subject;
            base.Body = body;

            string[] toArr = MailAddressCollectionToStringArray(to);
            string[] ccArr = cc != null ? MailAddressCollectionToStringArray(cc) : new string[] { "no one." };

            XmlData toData = new XmlData("to", string.Empty);
            XmlData ccData = new XmlData("cc", string.Empty);

            // Build the recipients data list
            for (int i = 0; i < toArr.Length; i++)
            {
                string email = toArr[i];
                toData.value += email + (i < toArr.Length - 2 ? "," : string.Empty);
            }

            // Build the forward data list
            for (int i = 0; i < ccArr.Length; i++)
            {
                string email = ccArr[i];
                ccData.value += email + (i < ccArr.Length - 2 ? "," : string.Empty);
            }

            //Now we have built the 2 xml data lists read for sending over the server send an email to each recipient / forwardee
            foreach (string email in toArr)
            {
                string xml = Utils.CreateXMLDocument("mail", new XmlData("recipient", email),
                                             new XmlData("from", from.Address),
                                             toData,
                                             ccData,
                                             new XmlData("subject", subject),
                                             new XmlData("body", body),
                                             new XmlData("forwarded", string.Concat(false)),
                                             new XmlData("read", string.Concat(false)));
                Form1.connection.Write("/sendmail " + xml);
            }

            if (cc != null)
            {
                foreach (string email in ccArr)
                {
                    string xml = Utils.CreateXMLDocument("mail", new XmlData("recipient", email),
                                                 new XmlData("from", from.Address),
                                                 toData,
                                                 ccData,
                                                 new XmlData("subject", subject),
                                                 new XmlData("body", body),
                                                 new XmlData("forwarded", string.Concat(true)),
                                                 new XmlData("read", string.Concat(false)));
                    Form1.connection.Write("/sendmail " + xml);
                }
            }
        }

        /// <summary>
        /// Convert a MailAddressCollection to a string array
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public string[] MailAddressCollectionToStringArray(MailAddressCollection collection)
        {
            List<string> cache = new List<string>();
            foreach(MailAddress address in collection)
            {
                cache.Add(address.Address);
            }
            return cache.ToArray();
        }
    }
}
