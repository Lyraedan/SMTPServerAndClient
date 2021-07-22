/*
 
    Author: Zel (Luke Rapkin)
    Student ID: 100527733
    Created: 01/11/2020
    Last Modified: 05/01/2021

 */

using System;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Net.Mail;

namespace SMTP
{
    public class Authenticator
    {

        public enum Response
        {
            SUCCESSFUL = 200,
            FAILURE = 403
        }

        public string email { get; private set; } = string.Empty;
        public string password { get; private set; } = string.Empty;

        public bool loggedin { get; private set; } = false;

        public Action OnLogin, OnLogout;

        public Response response = Response.FAILURE;

        // This is the hashing algorithm we will use for securing the password
        public SHA256 sha256;

        public Authenticator(Form1 form)
        {
            sha256 = SHA256.Create();

            // If the server returns a successful login event execute this code
            Form1.commandline.OnLoginSuccessful += res =>
            {
                response = Response.SUCCESSFUL;
                Form1.connection.client.UseDefaultCredentials = false;
                Form1.connection.client.Credentials = new NetworkCredential(this.email, this.password);
                loggedin = true;

                OnLogin?.Invoke();
                Console.WriteLine($"Logged in with:\nEmail: {email}\nPassword: {this.password}");
                Form1.connection.Write($"User logged in as {email}");
                // Load up their inbox
                form.RefreshInbox();
                MessageBox.Show("Logged in");
            };

            Form1.commandline.OnLoginFailed += res => MessageBox.Show(res);

            Form1.commandline.OnRegisterSuccessful += res => MessageBox.Show(res);

            Form1.commandline.OnRegisterFailed += res => MessageBox.Show(res);
        }

        /// <summary>
        /// Send the login credentials to the server to validate and login
        /// </summary>
        /// <param name="email">The users email</param>
        /// <param name="password">The users password - This gets hashed</param>
        public void Login(string email, string password, Panel inbox, Form1 form)
        {
            if (Form1.connection.connected)
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show($"Failed to login\nEmail or Password was empty!");
                    return;
                }

                // Validate the email address is a proper address
                try
                {
                    MailAddress address = new MailAddress(email);

                    this.email = email;
                    string hash = InputToHash(sha256, password);
                    this.password = hash;
                    Form1.userDb.Login(email, hash);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Failed to login\nAn invalid email address was provided!");
                    return;
                }
            }
            else MessageBox.Show("You are not connected to any SMTP server yet...");
        }

        /// <summary>
        /// Log the user out of the client
        /// </summary>
        public void Logout()
        {
            email = string.Empty;
            password = string.Empty;
            Form1.connection.client.Credentials = null;
            loggedin = false;

            OnLogout?.Invoke();
            response = Response.FAILURE;
        }

        /// <summary>
        /// Convert a regular string into a hashed hexadecimal string
        /// </summary>
        /// <param name="hashAlgorithm">The hashing algoritm to use</param>
        /// <param name="input">What input do we wish to hash</param>
        /// <returns>The input as hashed hexadecimal string</returns>
        public string InputToHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
