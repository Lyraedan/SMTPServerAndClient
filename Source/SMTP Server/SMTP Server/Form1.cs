using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace SMTP_Server
{
    public partial class Form1 : Form
    {
        // Main
        public Thread thread;

        public bool debugMode = false;

        public bool ready = false;
        public bool running = false;

        private TcpListener listener;
        private Thread serverThread;
        private DateTime start;

        #region singletons
        public static CommandLine commandline;
        public static Mailbox mailbox;
        public static UserDatabase userDb;
        public static Dictionary<long, SocketHandler> clients = new Dictionary<long, SocketHandler>();
        #endregion

        public enum Logger
        {
            LOG, ERROR, SERVER, DEBUG
        }

        public Form1()
        {
            InitializeComponent();
            WriteLine("SMTP Server - By Luke Rapkin\n");

            commandline = new CommandLine();
            mailbox = new Mailbox(this);
            userDb = new UserDatabase(this);

            button2.Text = running ? "Stop server" : "Start Server";
            Thread.CurrentThread.Name = "Thread";

            thread = new Thread(new ThreadStart(UpdateElements));
            thread.Name = "Main";
            thread.Start();

        }

        public void Start(string ip, int port)
        {
            if (running || !ready)
            {
                string message = "Failed to run server. ";
                if (running) message += "Server is already running...";
                else if (!ready) message += "Server is not ready!";
                WriteLine(message, Logger.ERROR);
                return;
            }
            running = true;

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            WriteLine($"Starting server on {ip}:{port}");
            listener = new TcpListener(endPoint);
            listener.Start();
            start = DateTime.UtcNow;
            WriteLine("Started server. Awaiting connections...");

            textBox1.Enabled = false;
            textBox2.Enabled = false;

            serverThread = new Thread(new ThreadStart(Run));
            serverThread.Name = "Server";
            serverThread.Start();

        }

        /// <summary>
        /// This run method has a while running check but it is halted and awaits a connection before proceeding.
        /// </summary>
        void Run()
        {
            while (running)
            {
                try
                {
                    SocketHandler socket = new SocketHandler();
                    socket.client = listener.AcceptTcpClient();
                    WriteLine($"[Socket/{socket.socketID}] Client Connected! Setting up listener...", Logger.SERVER);
                    socket.stream = socket.client.GetStream();
                    socket.serverInstance = new SMTPServer(this, socket);
                    socket.thread = new Thread(new ThreadStart(socket.serverInstance.Run));

                    #region Commandline listeners
                    socket.serverInstance.OnLoginRequested += (socketID, data) =>
                    {
                        string xml = data.Substring("/login ".Length).Trim();
                        try
                        {
                            XmlDocument document = new XmlDocument();
                            document.LoadXml(Utils.Decompile(xml));
                            var email = document.DocumentElement.Attributes["email"].Value;
                            var hash = document.DocumentElement.Attributes["hash"].Value;
                            Form1.userDb.Login(email, hash, (msg) =>
                            {
                                socket.serverInstance.Write(socketID, "login successful " + msg);
                            }, (msg) =>
                            {
                                socket.serverInstance.Write(socketID, "login failed " + msg);
                            });
                        }
                        catch (Exception e)
                        {
                            WriteLine("Failed to parse XML:\n" + xml);
                            socket.serverInstance.Write(socketID, $"login failed Something went wrong on the server.");
                        }
                    };

                    socket.serverInstance.OnRegisterRequested += (socketID, data) =>
                    {
                        string xml = data.Substring("/register ".Length).Trim();
                        try
                        {
                            XmlDocument document = new XmlDocument();
                            document.LoadXml(Utils.Decompile(xml));
                            var email = document.DocumentElement.Attributes["email"].Value;
                            var hash = document.DocumentElement.Attributes["hash"].Value;
                            userDb.Register(email, hash, (msg) =>
                            {
                                socket.serverInstance.Write(socketID, "registration successful " + msg);
                            }, (msg) =>
                            {
                                socket.serverInstance.Write(socketID, "registration failed " + msg);
                            });
                        }
                        catch (Exception e)
                        {
                            WriteLine("Failed to parse XML:\n" + xml);
                            socket.serverInstance.Write(socketID, $"registration failed Something went wrong on the server.");
                        }
                    };

                    socket.serverInstance.OnSendMailRequested += (socketID, data) =>
                    {
                        string xml = data.Substring("/sendmail ".Length).Trim();
                        try
                        {
                            XmlDocument document = new XmlDocument();
                            document.LoadXml(Utils.Decompile(xml));
                            var recipient = document.DocumentElement.Attributes["recipient"].Value;
                            var from = document.DocumentElement.Attributes["from"].Value;
                            var to = document.DocumentElement.Attributes["to"].Value.Split(',');
                            var cc = document.DocumentElement.Attributes["cc"].Value.Split(',');
                            var subject = document.DocumentElement.Attributes["subject"].Value;
                            var body = document.DocumentElement.Attributes["body"].Value;
                            var forwarded = document.DocumentElement.Attributes["forwarded"].Value;

                            mailbox.StoreMail(recipient, from, to, cc, subject, body, bool.Parse(forwarded));
                        }
                        catch (Exception e)
                        {
                            WriteLine("Failed to parse XML:\n" + xml);
                            socket.serverInstance.Write(socketID, $"Sending mail failed Something went wrong on the server.");
                        }
                    };

                    socket.serverInstance.OnUpdateInboxRequested += (socketID, data) =>
                    {
                        mailbox.LoadXMLDocument();
                        string xml = data.Substring("/updateinbox ".Length);
                        try
                        {
                            XmlDocument document = new XmlDocument();
                            document.LoadXml(Utils.Decompile(xml));
                            var email = document.DocumentElement.Attributes["email"].Value;

                            XmlDocument documentToSend = new XmlDocument();
                            XmlElement root = documentToSend.CreateElement("inbox");
                            string token = userDb.GetTokenFor(email);
                            List<InboxMail> inbox = mailbox.GetMailFor(token);
                            foreach (InboxMail mail in inbox)
                            {
                                string to = "";
                                for (int i = 0; i < mail.to.ToArray().Length; i++)
                                {
                                    string recipient = mail.to[i];
                                    to += recipient + (i < mail.to.ToArray().Length - 2 ? "," : string.Empty);
                                }
                                string cc = "";
                                if (mail.ccs.Count > 0)
                                {
                                    for (int i = 0; i < mail.ccs.ToArray().Length; i++)
                                    {
                                        string recipient = mail.ccs[i];
                                        cc += recipient + (i < mail.ccs.ToArray().Length - 2 ? "," : string.Empty);
                                    }
                                }

                                XmlElement element = documentToSend.CreateElement("mail");
                                element.SetAttribute("from", mail.from);
                                element.SetAttribute("to", to);
                                element.SetAttribute("cc", cc);
                                element.SetAttribute("subject", mail.subject);
                                element.SetAttribute("body", mail.body);
                                element.SetAttribute("forwarded", string.Concat(mail.forwarded));
                                element.SetAttribute("read", string.Concat(mail.read));
                                root.AppendChild(element);
                            }
                            documentToSend.AppendChild(root);

                            // Clean out any invalid characters that may be generated before sending it to the client
                            string finalXML = documentToSend.OuterXml.Replace("&#x0;", string.Empty);
                            socket.serverInstance.Write(socketID, "updated inbox " + email + " " + finalXML);
                        }
                        catch (Exception e)
                        {
                            WriteLine("Something went wrong Data:\n" + e.Data + "\nMessage: " + e.Message + "\nStacktrace: \n" + e.StackTrace);
                            socket.serverInstance.Write(socketID, $"Sending mail failed!");
                        }
                    };

                    #endregion
                    socket.thread.Start();
                    // Send the client its assigned socket id
                    socket.serverInstance.Write(-1, $"connect {socket.socketID}");
                    clients.Add(socket.socketID, socket);
                }
                catch (Exception e)
                {
                    if(debugMode)
                        Console.WriteLine($"Something went wrong waiting for clients!\n{e.Source}\n{e.Message}\n{e.StackTrace}");
                }
            };
        }

        /// <summary>
        /// This update method has a while running check but goes completely uninterrupted. Useful for updating GUI elements like timers.
        /// </summary>
        void UpdateElements()
        {
            while (true)
            {
                if (running)
                    UpdateTimeElapsed("Server run for: " + GetElapsedTime());
                else
                    UpdateTimeElapsed("Server is not running...");

                UpdateCurrentTime(DateTime.UtcNow.ToString());
            }
        }

        /*
            Because we are multi-threading and are going to be calling these functions from multiple threads we only want it to update the rich text box from the main thread.
            Doing it like this prevents the "Cross-thread operation not valid" exception
        */
        #region multi-threaded functions
        delegate void UpdateText(string msg, Logger tag = Logger.LOG);

        public void WriteLine(string msg, Logger tag = Logger.LOG)
        {
            if (tag.Equals(Logger.DEBUG) && !debugMode) return;

            string message = $"[{tag.ToString()}] {msg}\n";

            UpdateText dele = new UpdateText(WriteLine);
            InvokeDelegate(dele, new object[] { msg, tag }, richTextBox1.InvokeRequired, () =>
            {
                richTextBox1.Text += message;
                richTextBox1.Select(richTextBox1.Text.Length - 1, 0);
                richTextBox1.ScrollToCaret();
            });
        }

        public void UpdateTimeElapsed(string msg, Logger tag = Logger.LOG)
        {
            if (!ready) return;

            UpdateText method = new UpdateText(UpdateTimeElapsed);
            InvokeDelegate(method, new object[] { msg, tag }, label3.InvokeRequired, () => label3.Text = msg);
        }

        public void UpdateCurrentTime(string msg, Logger tag = Logger.LOG)
        {
            if (!ready) return;
            UpdateText method = new UpdateText(UpdateCurrentTime);
            InvokeDelegate(method, new object[] { msg, tag }, label4.InvokeRequired, () => label4.Text = msg);
        }
        #endregion

        /// <summary>
        ///  Custom invoking method that has error handling.
        /// </summary>
        /// <param name="method">The delegate method we want to reference</param>
        /// <param name="delegateParameters">What are the parameters of this delegate method</param>
        /// <param name="invokeRequired">Is invoking required?</param>
        /// <param name="onInvoked">What happens when the delegate is invoked?</param>
        private void InvokeDelegate(Delegate method, object[] delegateParameters, bool invokeRequired, Action onInvoked)
        {
            try
            {
                if(invokeRequired)
                {
                    Invoke(method, delegateParameters);
                } else
                {
                    onInvoked.Invoke();
                }
            } catch(Exception e)
            {
                // Print that the invoke failed, tell the user what method failed, what the exception is, what caused the error and finally show the stacktrace
                if(debugMode)
                    Console.WriteLine($"Failed to invoke delegate {method.Method.Name}!\n{e.Source}\n{e.Message}\n{e.StackTrace}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!running)
                Start(textBox1.Text, int.Parse(textBox2.Text));
            else
                Stop();

            button2.Text = running ? "Stop server" : "Start Server";
        }

        void Stop()
        {
            if (!running || !ready) return;
            running = false;
            WriteLine("Server shutting down");

            textBox1.Enabled = true;
            textBox2.Enabled = true;

            listener.Stop();
            serverThread.Interrupt();
        }

        public new DialogResult Show()
        {
            ready = true;
            return ShowDialog();
        }

        public string GetElapsedTime()
        {
            if (!running) return string.Empty;

            TimeSpan elapsed = DateTime.UtcNow - start;
            return (elapsed.Hours + " h " + elapsed.Minutes + " m " + elapsed.Seconds + " s");
        }
    }

    public class SocketHandler
    {
        public TcpClient client;
        public NetworkStream stream;
        public Thread thread;
        public long socketID = -1;
        public SMTPServer serverInstance;

        public SocketHandler()
        {
            this.socketID = Utils.NanoTime();
        }
    }

}