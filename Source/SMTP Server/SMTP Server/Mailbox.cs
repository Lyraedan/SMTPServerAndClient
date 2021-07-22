using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SMTP_Server
{
    public class Mailbox : XMLDoc
    {

        public Form1 form;

        public Mailbox(Form1 form)
        {
            this.form = form;
            path = "mail.xml";

            string xml = "<mailbox>\n</mailbox>";
            // If the users database doesn't exist create it.
            if (!File.Exists(path))
            {
                File.WriteAllText(path, xml);
                form.WriteLine("Mailbox doesn't exist. Creating...", Form1.Logger.ERROR);
            }
            else
            {
                form.WriteLine("Loading mailbox...");
            }
            LoadXMLDocument();
        }

        public List<InboxMail> GetMailFor(string token)
        {
            List<InboxMail> inbox = new List<InboxMail>();
            foreach (XmlNode node in root.ChildNodes)
            {
                var userToken = node.Attributes["token"].Value;
                if (token.Equals(userToken))
                {

                    foreach (XmlNode mailNode in node.ChildNodes)
                    {
                        // Decrypt the mail information
                        var from = Security.Decrypt(mailNode.Attributes["from"].Value);
                        var to = Security.Decrypt(mailNode.Attributes["to"].Value).Split(',');
                        var cc = Security.Decrypt(mailNode.Attributes["cc"].Value).Split(',');
                        var subject = Security.Decrypt(mailNode.Attributes["subject"].Value);
                        var body = Security.Decrypt(mailNode.Attributes["body"].Value);

                        var forwarded = mailNode.Attributes["forwarded"].Value;
                        var read = mailNode.Attributes["read"].Value;

                        // Add the decrypted data to the relivant inbox mail list
                        inbox.Add(new InboxMail(from, to, cc, subject, body, mailNode, bool.Parse(forwarded), bool.Parse(read)));
                    }
                    // We have now setup the inbox for the user break out the foreach
                    break;
                }
            }
            // Finally return the list of mail for the user with the access token
            return inbox;
        }

        public void StoreMail(string recipient, string from, string[] to, string[] ccs, string subject, string body, bool forwarded = false)
        {
            string token = Form1.userDb.GetTokenFor(recipient);
            // The token is found
            if (!token.Equals("N/A"))
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    var userToken = node.Attributes["token"].Value;
                    // Compare the this users token to that of the node in the database
                    if (token.Equals(userToken))
                    {
                        //A match was found, store this email
                        XmlElement element = document.CreateElement("mail");
                        element.SetAttribute("from", Security.Encrypt(from));
                        string toAttribute = string.Empty;
                        for (int i = 0; i < to.Length; i++)
                        {
                            toAttribute += to[i] + (i < to.Length ? "," : string.Empty);
                        }
                        element.SetAttribute("to", Security.Encrypt(toAttribute));
                        if (ccs.Length >= 1)
                        {
                            string ccAttribute = string.Empty;
                            for (int i = 0; i < ccs.Length; i++)
                            {
                                ccAttribute += ccs[i] + (i < ccs.Length ? ", " : string.Empty);
                            }
                            element.SetAttribute("cc", Security.Encrypt(ccAttribute));
                        }
                        element.SetAttribute("subject", Security.Encrypt(subject));
                        element.SetAttribute("body", Security.Encrypt(body));
                        element.SetAttribute("forwarded", string.Concat(forwarded));
                        element.SetAttribute("read", string.Concat(false));
                        // Add the new mail element to the users inbox node
                        node.AppendChild(element);
                        SaveXMLDocument("mailbox");
                        LoadXMLDocument();
                        break;
                    }
                }
            }
            else
            {
                if (!recipient.Equals("no one."))
                    Console.WriteLine($"Failed to store mail for {recipient} token not found.");
            }
        }
    }

    public class InboxMail
    {
        public string from;
        public List<string> to = new List<string>();
        public List<string> ccs = new List<string>();
        public string subject = string.Empty;
        public string body = string.Empty;
        public XmlNode node;
        public bool forwarded = false;
        public bool read = false;

        public InboxMail(string from, string[] to, string[] ccs, string subject, string body, XmlNode mailNode, bool forwarded, bool read)
        {
            this.from = from;
            foreach (string recipient in to)
            {
                this.to.Add(recipient);
            }
            foreach (string recipient in ccs)
            {
                this.ccs.Add(recipient);
            }
            this.subject = subject;
            this.body = body;
            this.node = mailNode;
            this.forwarded = forwarded;
            this.read = read;
        }
    }
}
