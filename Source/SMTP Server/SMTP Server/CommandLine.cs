using System;
using System.Text;
using System.Xml;

namespace SMTP_Server
{
    public class CommandLine
    {

        public readonly string PREFIX = "/";
        
        /// <summary>
        /// What should the server do when it recieves an event from the server
        /// </summary>
        /// <param name="socketID">Which client made the request to the server</param>
        /// <param name="data">What does the packet data read</param>
        public void Parse(long socketID, string data)
        {
            if (data.StartsWith($"{PREFIX}login"))
            {
                Form1.clients[socketID].serverInstance.OnLoginRequested?.Invoke(socketID, data);
            }
            else if (data.StartsWith($"{PREFIX}register"))
            {
                Form1.clients[socketID].serverInstance.OnRegisterRequested?.Invoke(socketID, data);
            }
            else if (data.StartsWith($"{PREFIX}sendmail"))
            {
                Form1.clients[socketID].serverInstance.OnSendMailRequested?.Invoke(socketID, data);
            }
            else if (data.StartsWith($"{PREFIX}updateinbox"))
            {
                Form1.clients[socketID].serverInstance.OnUpdateInboxRequested?.Invoke(socketID, data);
            }
        }

    }
}
