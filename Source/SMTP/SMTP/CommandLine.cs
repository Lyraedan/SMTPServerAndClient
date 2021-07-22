using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 
    Author: Zel (Luke Rapkin)
    Student ID: 100527733
    Created: 30/12/2021
    Last Modified: 05/01/2021

 */

namespace SMTP
{
    public class CommandLine
    {
        public readonly string PREFIX = "/";

        public Action<string> OnLoginSuccessful, OnLoginFailed;
        public Action<string> OnRegisterSuccessful, OnRegisterFailed;
        public Action<string, string> OnInboxUpdated;

        /// <summary>
        /// What do you want the client to do with the information recieved from the server?
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            if (data.StartsWith("login successful"))
            {
                OnLoginSuccessful?.Invoke(data.Substring("login successful".Length));
            }
            else if (data.StartsWith("login failed"))
            {
                OnLoginFailed?.Invoke(data.Substring("login failed".Length));
            }
            else if (data.StartsWith("registration successful"))
            {
                OnRegisterSuccessful?.Invoke(data.Substring("registration successful".Length));
            }
            else if (data.StartsWith("registration failed"))
            {
                OnRegisterFailed?.Invoke(data.Substring("registration failed".Length));
            } else if(data.StartsWith("updated inbox"))
            {
                // Trim off the command text and extract the required data
                string email = data.Substring(14).Split(' ')[0];
                string xml = data.Substring(14 + email.Length + 1);
                OnInboxUpdated?.Invoke(email, Utils.Decompile(xml));
            } else if(data.StartsWith("Sending mail failed"))
            {
                MessageBox.Show(data.Substring("Sending mail failed ".Length));
            } else if(data.StartsWith("connect"))
            {
                // If we are not connected assign the users unique socket id on the client
                if (Form1.connection.socketID == -1)
                {
                    string recieved = data.Split(' ')[1];
                    Form1.connection.socketID = long.Parse(recieved);
                }
            }
        }
    }
}
