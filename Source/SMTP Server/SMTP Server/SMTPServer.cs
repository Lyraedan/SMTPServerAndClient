using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SMTP_Server
{
    public class SMTPServer
    {

        private Form1 form;
        private SocketHandler socket;

        // What do we do when the server recieves data
        public Action<string> OnDataRecieved;
        // What should the server do when it recieves any of these requests
        public Action<long, string> OnLoginRequested, OnRegisterRequested, OnSendMailRequested, OnUpdateInboxRequested;

        public SMTPServer(Form1 form, SocketHandler socket)
        {
            this.form = form;
            this.socket = socket;

            OnDataRecieved += data =>
            {
                Form1.commandline.Parse(socket.socketID, data);
            };
        }

        public void Run()
        {
            byte[] packet = new byte[1024];
            while (true)
            {
                try
                {
                    // Read packet data from the client
                    int i = socket.stream.Read(packet, 0, packet.Length);
                    while (i != 0)
                    {
                        // Convert that packet into something readable
                        string data = Read(packet);
                        string id = data.Split(' ')[0];
                        // Check the id of client who sent the event
                        if (socket.socketID == long.Parse(id))
                        {
                            // Remove the id from the command so its ready to be executed
                            string parsed = data.Substring(id.Length + 1);
                            OnDataRecieved.Invoke(parsed);
                        }
                        // Flush out the packet array
                        packet = new byte[1024];
                        // Read packet data from the client
                        i = socket.stream.Read(packet, 0, packet.Length);
                    }
                }
                catch (Exception e)
                {
                    form.WriteLine($"A users connection was abruptly closed!", Form1.Logger.SERVER);
                    socket.client.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// Write data back to the client
        /// </summary>
        /// <param name="socketID">The ID of the client recieving the message</param>
        /// <param name="message">What the message is</param>
        /// <returns></returns>
        public byte[] Write(long socketID, string message)
        {
            //if (!socket.canSend) return Encoding.ASCII.GetBytes(string.Empty);
            try
            {
                // Encode the message for the client adding the socket who sent the message id at the beginning
                byte[] packet = Encoding.ASCII.GetBytes($"{socketID} {message}");
                socket.stream.Write(packet, 0, packet.Length);
                socket.stream.Flush();
                return packet;
            }
            catch (Exception e)
            {
                form.WriteLine($"Failed to write message {message}\n{e.Message}", Form1.Logger.ERROR);
            }
            finally
            {
                //stream.Close();
            }
            return Encoding.ASCII.GetBytes(string.Empty);
        }

        /// <summary>
        /// Read a packet from the client
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public string Read(byte[] packet)
        {
            try
            {
                string data = Encoding.ASCII.GetString(packet);
                if (!string.IsNullOrEmpty(data))
                {
                    form.WriteLine(data, Form1.Logger.SERVER);
                }
                return data;
            }
            catch (Exception e)
            {
                form.WriteLine($"Failed to read data\n{e.Message}\n{e.StackTrace}", Form1.Logger.ERROR);
            }
            return string.Empty;
        }
    }
}
