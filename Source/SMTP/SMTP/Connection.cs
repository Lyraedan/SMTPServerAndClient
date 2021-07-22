/*
 
    Author: Zel (Luke Rapkin)
    Student ID: 100527733
    Created: 01/11/2020
    Last Modified: 05/01/2021

 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMTP
{
    public class Connection
    {
        public Thread thread;

        public TcpClient tcpClient { get; private set; }
        public NetworkStream stream { get; private set; }

        // This is the server ip address
        public string server = "127.0.0.1";
        public int port = 25; // 587, 465, 25

        // Do we want to utilise a secure socket layer?
        public bool enableSSL = false;

        public SmtpClient client { get; private set; }
        public bool connected { get; private set; }

        public Action<string> OnDataRecieved;

        public long socketID = -1;

        public Connection()
        {
            // What to do when the server
            OnDataRecieved += data =>
            {
                Form1.commandline.Parse(data);
            };
        }

        /// <summary>
        /// Connect to a server either with or without SSL
        /// </summary>
        /// <param name="server">The IP Address of the server</param>
        /// <param name="port">The port the server is running on</param>
        /// <param name="enableSSL">Use a secure socket layer - Default: false</param>
        public void Connect(string server, int port, bool enableSSL = false)
        {
            this.server = server;
            this.port = port;
            this.enableSSL = enableSSL;

            try
            {
                client = new SmtpClient(server);
                client.Port = port;

                if (!enableSSL) ConnectTCP(server, port);
                else ConnectTCPSSL(server, port);

                connected = true;

                thread = new Thread(new ThreadStart(Listen));
                // Marking the thread as a background thread will make it automatically close on application exit
                thread.IsBackground = true;
                thread.Start();
                MessageBox.Show("Connected to server");
            }
            catch (Exception e)
            {
                connected = false;
                MessageBox.Show($"Failed to connect to server {server}:{port}\n{e.Message}");
            }
        }

        public void Listen()
        {
            byte[] packet = new byte[1024];
            while (true)
            {
                try
                {
                    //Read the packets recieved from the server
                    int i = stream.Read(packet, 0, packet.Length);
                    while (i != 0)
                    {
                        // Convert that data into a readable and parsable format
                        string data = Read(packet);
                        string id = data.Split(' ')[0];
                        // Compare the socket ID of the caller and see if it matches our clients assigned socket id. If not invoke the OnDataRecieved event
                        if (socketID == long.Parse(id))
                        {
                            OnDataRecieved?.Invoke(data.Substring(id.Length + 1));
                        }
                        // Clear out the packet byte array
                        packet = new byte[1024];
                        // Read the next flow of data coming from the server
                        i = stream.Read(packet, 0, packet.Length);
                    }
                }
                catch (Exception e)
                {
                    // Connection to the server was lost
                    MessageBox.Show("You have been disconnected from the server.");
                    Console.Write(e.Data + "\n" + e.Message + "\n" + e.StackTrace);
                    Disconnect(true);
                    break;
                }
            }
        }

        /// <summary>
        /// Connect the client to a TCP server via a ip and port
        /// </summary>
        /// <param name="server">The IP address of the server</param>
        /// <param name="port">The port of the server (587, 465, 25)</param>
        void ConnectTCP(string server, int port)
        {
            tcpClient = new TcpClient();

            IPAddress ipAddress = IPAddress.Parse(server);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            tcpClient.Connect(ipEndPoint);
            stream = tcpClient.GetStream();
        }

        /// <summary>
        /// Connect the client to a TCP server via a ip and port using a Secure Socket Layer.
        /// </summary>
        /// <param name="server">The IP address of the server</param>
        /// <param name="port">The port of the server (587, 465, 25)</param>
        void ConnectTCPSSL(string server, int port)
        {
            tcpClient = new TcpClient();

            IPAddress ipAddress = IPAddress.Parse(server);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            tcpClient.Connect(ipEndPoint);
            stream = tcpClient.GetStream();
        }

        /// <summary>
        /// Disconnect the user from the server
        /// </summary>
        public void Disconnect(bool wasForcefullyDisconnected = false)
        {
            if (!wasForcefullyDisconnected)
                Form1.connection.Write($"User disconnected");
            client.Dispose();
            stream.Close();
            tcpClient.Close();
            thread.Interrupt();
            connected = false;
        }

        /// <summary>
        /// Write data to the server as a packet
        /// </summary>
        /// <param name="message"></param>
        /// <returns>The input message as a byte array packet. If it fails return an empty byte array</returns>
        public byte[] Write(string message)
        {
            try
            {
                byte[] packet = Encoding.ASCII.GetBytes($"{Form1.connection.socketID} {message}\n");
                stream.Write(packet, 0, packet.Length);
                stream.Flush();
                return packet;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] Failed to write message {message}\n{e.Message}");
            }
            return Encoding.ASCII.GetBytes(string.Empty);
        }

        /// <summary>
        /// Read data recieved from the server
        /// </summary>
        /// <param name="packet">The byte array of data to be read</param>
        /// <returns>The byte array packet as a string. If this fails returns an empty string</returns>
        public string Read(byte[] packet)
        {
            try
            {
                string data = Encoding.ASCII.GetString(packet);
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] Failed to read data\n{e.Message}");
            }
            return string.Empty;
        }

        /// <summary>
        /// Send an email from your account to a recipient and and linked CCs with a subject and body.
        /// </summary>
        /// <param name="from">The email the message is coming from</param>
        /// <param name="to">Who is this email going to (Seperate with commas)</param>
        /// <param name="ccs">Who is CCed in this email (Seperate with commas)</param>
        /// <param name="subject">What is the subject of the email</param>
        /// <param name="body">The main body of the email</param>
        /// <param name="forcefullyForward">Do we want to mark the email as forwarded upon arrival</param>
        public void Send(string from, string[] to, string[] ccs, string subject, string body, bool forcefullyForward = false)
        {
            try
            {
                Email email = new Email();
                email.From(from);
                email.To(to);
                if (ccs.Length > 0)
                    email.CC(ccs);
                email.Send(subject, body, forcefullyForward);

                MessageBox.Show($"Email successfully sent to recipients!");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to send email!\nPlease check the recipients and/or CC addresses are valid.");
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
