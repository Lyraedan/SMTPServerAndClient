using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SMTP_Server
{
    public class UserDatabase : XMLDoc
    {

        public Form1 form;

        public UserDatabase(Form1 form)
        {
            this.form = form;
            path = "users.xml";

            string xml = "<users>\n</users>";
            // If the users database doesn't exist create it.
            if (!File.Exists(path))
            {
                File.WriteAllText(path, xml);
                form.WriteLine("User database doesn't exist. Creating...", Form1.Logger.ERROR);
            } else
            {
                form.WriteLine("Loading userdatabase...");
            }
            LoadXMLDocument();
        }

        /// <summary>
        /// Use this to validate and log the user into an account
        /// </summary>
        public void Login(string email, string hash, Action<string> OnLogin, Action<string> OnLoginFailed)
        {
            bool found = false;
            bool loginSuccessful = false;
            foreach (XmlNode node in root.ChildNodes)
            {
                string storedEmail = node.Attributes["email"].Value;
                string storedHash = node.Attributes["hash"].Value;
                if (storedEmail.Equals(email))
                {
                    found = true;
                    if (hash.Equals(storedHash))
                    {
                        loginSuccessful = true;
                        // Only invoke OnLogin if it is not null!
                        OnLogin?.Invoke("Logged in");
                    }
                    break;
                }
            }
            if (!found)
            {
                OnLoginFailed?.Invoke("No account was found associated with that email!");
                return;
            }
            if (!loginSuccessful)
            {
                OnLoginFailed?.Invoke("Login was unsuccessful please check email/password fields.");
                return;
            }
        }

        /// <summary>
        /// Extract the access token for a specific email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetTokenFor(string email)
        {
            foreach (XmlNode node in root.ChildNodes)
            {
                var storedEmail = node.Attributes["email"].Value;
                if (email.Equals(storedEmail))
                {
                    var storedToken = node.Attributes["token"].Value;
                    return storedToken;
                }
            }
            return "N/A";
        }

        /// <summary>
        /// Use this to register an account on the database
        /// </summary>
        public void Register(string email, string hash, Action<string> OnRegisterSuccessful, Action<string> OnRegisterFailed)
        {
            bool exists = false;
            // Check if the user has an account already
            foreach (XmlNode node in root.ChildNodes)
            {
                string storedEmail = node.Attributes["email"].Value;
                if (storedEmail.Equals(email))
                {
                    exists = true;
                    break;
                }
            }
            // If the user doesn't have an account create one.
            if (!exists)
            {
                //Create the new account here
                XmlElement element = document.CreateElement("user");
                string authToken = GenerateAuthToken();
                element.SetAttribute("email", email);
                element.SetAttribute("hash", hash);
                element.SetAttribute("token", authToken);
                root.AppendChild(element);
                SaveXMLDocument("users");

                //Now setup the mailbox
                XmlElement mailbox = Form1.mailbox.document.CreateElement("inbox");
                mailbox.SetAttribute("token", authToken);
                Form1.mailbox.root.AppendChild(mailbox);
                Form1.mailbox.SaveXMLDocument("mailbox");
                OnRegisterSuccessful?.Invoke("Account successfully registered!");
            }
            else
            {
                OnRegisterFailed?.Invoke("That email is already associated with an account!");
            }
        }

        /// <summary>
        /// Generate the users access token. This is how they access their inboxes
        /// </summary>
        /// <returns></returns>
        private string GenerateAuthToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
