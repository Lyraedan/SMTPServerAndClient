using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SMTP
{
    public static class Utils
    {
        /// <summary>
        /// By default the xml is written all in one line. Format that one line into a beautified version of xml - Makes it more readable
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>A beautified version of the input xml</returns>
        public static string BeautifyXML(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                document.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }
            return sb.ToString();
        }

        /// <summary>
        /// Because the Tab Controls hide the Enabled variable
        /// </summary>
        /// <param name="page"></param>
        /// <param name="enable"></param>
        public static void EnableTab(TabPage page, bool enable)
        {
            foreach (Control components in page.Controls)
            {
                components.Enabled = enable;
            }
        }

        /// <summary>
        /// Forcefully change the active tab
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="tab"></param>
        public static void ChangeTab(TabControl controller, TabPage tab)
        {
            controller.SelectTab(tab);
        }

        /// <summary>
        /// Build a custom input dialog window
        /// </summary>
        /// <param name="title">What will be displayed at the top of the popup</param>
        /// <param name="input">What string will we be setting to the input of the dialog box</param>
        /// <returns>A dialog window with a input box</returns>
        public static DialogResult ShowInputDialog(string title, ref string input)
        {
            // Setup the window and create our form
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = title;

            //Create and setup our text box
            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            // Create and setup the ok button
            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            // Create and setup the cancel button
            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            // Finally show the window and return the result
            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        /// <summary>
        /// Generate an XML document to send over the server
        /// </summary>
        /// <param name="tag">What is the element name in xml</param>
        /// <param name="data">What are the attributes for that tag</param>
        /// <returns>A serialised XML document</returns>
        public static string CreateXMLDocument(string tag, params XmlData[] data)
        {
            XmlDocument document = new XmlDocument();
            XmlElement element = document.CreateElement(tag);
            foreach(XmlData attribute in data)
            {
                element.SetAttribute(attribute.tag, attribute.value);
            }
            return element.OuterXml;
        }

        public static string Decompile(string str)
        {
            return str.Replace("\x00", string.Empty).Replace("\0", string.Empty).Replace("&#x0", string.Empty);
        }
    }

    public class XmlData
    {
        public string tag = string.Empty;
        public string value = string.Empty;

        public XmlData(string tag, string value)
        {
            this.tag = tag;
            this.value = value;
        }
    }
}
