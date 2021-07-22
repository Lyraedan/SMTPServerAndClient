using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace SMTP_Server
{
    class Utils
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
        /// Clean out any corrupt / invalid characters from the string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decompile(string str)
        {
            return str.Replace("\x00", string.Empty).Replace("\0", string.Empty).Replace("&#x0;", string.Empty);
        }

        /// <summary>
        /// Get the system nanotime in miliseconds
        /// </summary>
        /// <returns>Nanotime in ms</returns>
        public static long NanoTime()
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }
        /// <summary>
        /// Generate an XML document to send over the server
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CreateXMLDocument(string tag, params XmlData[] data)
        {
            XmlDocument document = new XmlDocument();
            XmlElement element = document.CreateElement(tag);
            foreach (XmlData attribute in data)
            {
                element.SetAttribute(attribute.tag, attribute.value);
            }
            return element.OuterXml;
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
