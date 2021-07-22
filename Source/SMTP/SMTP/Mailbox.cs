using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

/*
 
    Author: Zel (Luke Rapkin)
    Student ID: 100527733
    Created: 06/12/2020
    Last Modified: 23/12/2021

 */

namespace SMTP
{
    public class Mailbox : XMLDoc
    {
        /// <summary>
        /// This is the users inbox node root
        /// </summary>
        public XmlNode inboxNode;
        /// <summary>
        /// This is the inbox mail instance for the selected email. This contains everything about the email, senders, ccs, subject, body etc
        /// </summary>
        public InboxMail selectedMail;
        public Label selected;

        public Mailbox()
        {

        }

        [Obsolete("Not enough time to make port it to server based")]
        public void MarkAsRead(InboxMail mail, string token, Panel listDisplay, Form1 form, Label entry)
        {
            if (!mail.read)
            {
                selectedMail.node.Attributes["read"].InnerText = string.Concat(true);
                SaveXMLDocument("mailbox");
                mail.read = true;
                entry.Font = new Font(Control.DefaultFont, mail.read ? FontStyle.Regular : FontStyle.Bold);
            }
        }

        public void GenerateInboxFromServer(string email, string xml, Panel listDisplay, Form1 form)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                XmlNode root = document.ChildNodes[0];
                MessageBox.Show("Found " + root.ChildNodes.Count + " mail");
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode node = root.ChildNodes[i];
                    var from = node.Attributes["from"].Value;
                    var to = node.Attributes["to"].Value;
                    var cc = node.Attributes["cc"].Value;
                    var subject = node.Attributes["subject"].Value;
                    var body = node.Attributes["body"].Value;
                    var forwarded = bool.Parse(node.Attributes["forwarded"].Value);
                    var read = bool.Parse(node.Attributes["read"].Value);

                    Label entry = new Label
                    {
                        //Font = new Font(Control.DefaultFont, read ? FontStyle.Regular : FontStyle.Bold)
                    };
                    entry.Text = subject;
                    entry.ForeColor = Color.Blue;
                    // When we select our email  highlight it
                    entry.Click += delegate {
                        if (selected != null)
                        {
                            selected.ForeColor = Color.Blue;
                        }
                        form.InboxClicked(from, to, cc, subject, body);
                        entry.ForeColor = Color.Yellow;
                        selected = entry;
                        selectedMail = new InboxMail(from, to.Split(','), cc.Split(','), subject, body, node, forwarded, read);
                        //MarkAsRead(mail, token, listDisplay, form, entry);
                    };
                    entry.Location = new Point(entry.Location.X, entry.Location.Y + (24 * i));
                    form.UpdateInbox(entry);
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Data);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
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
            foreach(string recipient in to)
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
