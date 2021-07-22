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
    public class UserDatabase : XMLDoc
    {

        public UserDatabase()
        {
           
        }

        /// <summary>
        /// Make a login request to the server
        /// </summary>
        public void Login(string email, string hash)
        {
            if(!Form1.connection.connected)
            {
                MessageBox.Show("You are not connected to any SMTP Server.");
                return;
            }
            string xml = Utils.CreateXMLDocument("login", new XmlData("email", email), new XmlData("hash", hash));
            Form1.connection.Write($"/login {xml}");
        }

        /// <summary>
        /// Make a account registration request to the server
        /// </summary>
        public void Register(string email, string hash)
        {
            if (!Form1.connection.connected)
            {
                MessageBox.Show("You are not connected to any SMTP Server.");
                return;
            }
            string xml = Utils.CreateXMLDocument("register", new XmlData("email", email), new XmlData("hash", hash));
            Form1.connection.Write($"/register {xml}");
        }
    }
}
