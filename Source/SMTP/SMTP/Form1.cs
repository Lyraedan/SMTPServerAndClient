/*
 
    Author: Zel (Luke Rapkin)
    Student ID: 100527733
    Created: 01/11/2020
    Last Modified: 02/11/2020

 */


using System;
using System.Windows.Forms;

namespace SMTP
{
    public partial class Form1 : Form
    {

        /*
         
            This code use to be clean until i moved the xml database handling over to the server instead of the client

         */

        #region Singletons
        public static Connection connection;
        public static Authenticator auth;
        public static UserDatabase userDb;
        public static Mailbox mailbox;
        public static CommandLine commandline;
        #endregion

        #region Actions
        public static Action onConnectClicked, onLoginClicked, onSendClicked, onRegisterClicked;
        #endregion

        public Form1()
        {
            InitializeComponent();

            commandline = new CommandLine();
            connection = new Connection();
            auth = new Authenticator(this);
            userDb = new UserDatabase();
            mailbox = new Mailbox();

            // Delegates

            onConnectClicked = delegate
            {
                if (!connection.connected)
                {
                    connection.Connect(Server.Text, int.Parse(Port.Text), SSLEnabled.Checked);
                    if (connection.connected)
                    {
                        ConnectedStatus.Text = "Status: Connected" + (SSLEnabled.Checked ? " (SSL)" : string.Empty) + $" | ID: {connection.socketID}";
                        ConnectButton.Text = "Disconnect";
                        Server.Enabled = false;
                        Port.Enabled = false;
                    }
                } else
                {
                    connection.Disconnect();
                    if (!connection.connected)
                    {
                        ConnectButton.Text = "Connect";
                        if (auth.loggedin)
                        {
                            auth.Logout();
                            Utils.EnableTab(tabPage1, false);
                            InboxDisplay.Controls.Clear();
                        }
                        Server.Enabled = true;
                        Port.Enabled = true;
                        ConnectedStatus.Text = "Status: Not connected!";
                    }
                }
            };

            auth.OnLogin = delegate
            {
                DelegateAction action = new DelegateAction(GUILogin);
                InvokeDelegate(action, new object[] { }, true, () => { });
            };

            auth.OnLogout = delegate
            {
                DelegateAction action = new DelegateAction(GUILogout);
                InvokeDelegate(action, new object[] { }, true, () => { });
            };

            onLoginClicked = delegate
            {
                if (!auth.loggedin)
                {
                    auth.Login(Email.Text, Password.Text, InboxDisplay, this);
                }
                else
                {
                    auth.Logout();
                    BtnDelete.Enabled = false;
                    BtnForward.Enabled = false;
                    BtnReply.Enabled = false;
                    BtnRefresh.Enabled = false;
                    Utils.EnableTab(tabPage1, false);
                    InboxDisplay.Controls.Clear();
                }
            };

            onSendClicked = delegate
            {
                connection.Send(auth.email, ToBox.Text.Split(','), CCBox.Text.Split(','), SubjectBox.Text, BodyBox.Text);
            };

            onRegisterClicked = delegate
            {
                string email = RegisterEmail.Text;
                string password = RegisterPassword.Text;
                string hash = auth.InputToHash(auth.sha256, password);
                userDb.Register(email, hash);
            };

            commandline.OnInboxUpdated += (email, xml) =>
            {
                if (connection.connected && auth.loggedin)
                {
                    if(auth.email.Equals(email))
                    {
                        mailbox.GenerateInboxFromServer(email, xml, InboxDisplay, this);
                    }
                }
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Utils.EnableTab(tabPage1, false);
            BtnDelete.Enabled = false;
            BtnForward.Enabled = false;
            BtnReply.Enabled = false;
            BtnRefresh.Enabled = false;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            onSendClicked.Invoke();
        }

        public void InboxClicked(string from, string to, string cc, string subject, string body)
        {
            DelegateAction action = new DelegateAction(() =>
            {
                InboxFrom.Text = from;
                InboxTo.Text = to;
                InboxCC.Text = cc;
                SubjectDisplay.Text = subject;
                BodyDisplay.Text = body;
            });
            InvokeDelegate(action, new object[] { }, true, () => { });
        }

        delegate void DelegateAction();

        /// <summary>
        /// Use this to invoke methods from multiple threads.
        /// </summary>
        /// <param name="method">The delegate method we want to reference</param>
        /// <param name="delegateParameters">What are the parameters of this delegate method</param>
        /// <param name="invokeRequired">Is invoking required?</param>
        /// <param name="onInvoked">What happens when the delegate is invoked?</param>
        public void InvokeDelegate(Delegate method, object[] delegateParameters, bool invokeRequired, Action onInvoked)
        {
            try
            {
                if (invokeRequired)
                {
                    Invoke(method, delegateParameters);
                }
                else
                {
                    onInvoked.Invoke();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to invoke delegate {method.Method.Name}!\n{e.Source}\n{e.Message}\n{e.StackTrace}");
            }
        }

        /// <summary>
        /// Update the inbox list display through the main thread
        /// </summary>
        /// <param name="entry"></param>
        internal void UpdateInbox(Label entry)
        {
            DelegateAction action = new DelegateAction(() => {
                InboxDisplay.Controls.Add(entry);
            });
            InvokeDelegate(action, new object[] { }, InboxDisplay.InvokeRequired, () => { });
        }

        /// <summary>
        /// Make a request to the server to refresh our inbox
        /// </summary>
        public void RefreshInbox()
        {
            if (connection.connected && auth.loggedin)
            {
                InboxDisplay.Controls.Clear();
                string xml = Utils.CreateXMLDocument("mail", new XmlData("email", auth.email));
                connection.Write("/updateinbox " + xml);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshInbox();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //Delete email - can not be clicked unless logged in
            if (mailbox.selectedMail != null)
            {
                mailbox.selectedMail = null;
                InboxFrom.Text = string.Empty;
                InboxTo.Text = string.Empty;
                InboxCC.Text = string.Empty;
                SubjectDisplay.Text = string.Empty;
                BodyDisplay.Text = string.Empty;
                RefreshInbox();
            }
            else
                MessageBox.Show("Failed to delete email. None selected!");
        }

        private void BtnForward_Click(object sender, EventArgs e)
        {
            //Forward email - can not be clicked unless logged in
            if(mailbox.selectedMail != null)
            {
                string fwdTo = string.Empty;
                DialogResult result = Utils.ShowInputDialog("Forward", ref fwdTo);
                if (result == DialogResult.OK)
                {
                    connection.Send(auth.email,
                                    fwdTo.Split(','),
                                    new string[] { },
                                    "FWD: " + mailbox.selectedMail.subject,
                                    $"Email from {mailbox.selectedMail.from} forwarded by {auth.email}\n\n{mailbox.selectedMail.body}",
                                    true);
                    connection.Write($"{auth.email} forwarded an email to " + fwdTo);
                }
            }
            else
                MessageBox.Show("Failed to forward email. None selected!");
        }

        private void BtnReply_Click(object sender, EventArgs e)
        {
            //Reply to email - can not be clicked unless logged in
            if (mailbox.selectedMail != null)
            {
                string to = string.Empty;
                for (int i = 0; i < mailbox.selectedMail.to.Count; i++)
                {
                    to += mailbox.selectedMail.to[i] + (i < mailbox.selectedMail.to.Count - 1 ? "," : string.Empty);
                }
                ToBox.Text = to;
                string cc = string.Empty;
                for (int i = 0; i < mailbox.selectedMail.ccs.Count; i++)
                {
                    cc += mailbox.selectedMail.ccs[i] + (i < mailbox.selectedMail.ccs.Count - 1 ? "," : string.Empty);
                }
                CCBox.Text = cc;
                SubjectBox.Text = "RE: " + mailbox.selectedMail.subject;
                BodyBox.Text = string.Empty;
                Utils.ChangeTab(Page, tabPage1);
                connection.Write($"{auth.email} is replying to {to}");
            }
            else
                MessageBox.Show("Failed to reply to email. None selected!");
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            onRegisterClicked.Invoke();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            onConnectClicked?.Invoke();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            onLoginClicked?.Invoke();
        }

        #region delegate callbacks
        /// <summary>
        /// Used to update the components to the logged in state - Called from server thread
        /// </summary>
        void GUILogin() {
            LoginButton.Text = "Logout";
            Utils.EnableTab(tabPage1, true);
            InboxDisplay.Controls.Clear();
            // Disabled due to time constraints
            BtnDelete.Enabled = false;
            BtnForward.Enabled = false;
            BtnReply.Enabled = false;
            CCBox.Enabled = false;
            BtnRefresh.Enabled = true;
            Email.Enabled = false;
            Password.Enabled = false;
            ConnectedStatus.Text += $" | Logged in as {auth.email}";
        }

        /// <summary>
        /// Used to update the components to the logged in state - Called from server thread
        /// </summary>
        void GUILogout()
        {
            LoginButton.Text = "Login";
            Utils.EnableTab(tabPage1, false);
            BtnDelete.Enabled = false;
            BtnForward.Enabled = false;
            BtnReply.Enabled = false;
            BtnRefresh.Enabled = false;
            InboxDisplay.Controls.Clear();
            Email.Enabled = true;
            Password.Enabled = true;
            ConnectedStatus.Text = connection.connected ? $"Status: Connected | ID: {connection.socketID}" : "Status: Not Connected";
        }
        #endregion
    }
}
