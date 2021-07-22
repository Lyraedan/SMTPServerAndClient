using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SMTP
{
    public class XMLDoc
    {

        protected string path = "doc.xml";
        public XmlDocument document;
        public XmlNode root;

        /// <summary>
        /// Load up the database document and assign the root
        /// </summary>
        protected void LoadXMLDocument()
        {
            string xml = File.ReadAllText(path);
            document = new XmlDocument();
            document.LoadXml(xml);
            root = document.ChildNodes[0];
        }


        public void SaveXMLDocument(string rootTag)
        {
            string xml = $"<{rootTag}>{root.InnerXml}</{rootTag}>";
            File.WriteAllText(path, Utils.BeautifyXML(xml));
            Console.WriteLine("Saved document!");
        }
    }
}
