using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SMTP_Server
{
    public class XMLDoc
    {

        protected string path = "doc.xml";
        public XmlDocument document;
        public XmlNode root;

        /// <summary>
        /// Load up the database document and assign the root
        /// </summary>
        public void LoadXMLDocument()
        {
            string xml = File.ReadAllText(path);
            document = new XmlDocument();
            document.LoadXml(xml);
            root = document.ChildNodes[0];
        }

        /// <summary>
        /// Save the xml document within its root tag
        /// </summary>
        /// <param name="rootTag"></param>
        public void SaveXMLDocument(string rootTag)
        {
            string xml = $"<{rootTag}>{root.InnerXml}</{rootTag}>";
            File.WriteAllText(path, Utils.BeautifyXML(xml));
            Console.WriteLine("Saved document!");
        }
    }
}
