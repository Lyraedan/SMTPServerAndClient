using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMTP_Server
{

    class Program
    {
        static void Main(string[] args)
        {
            Form1 form = new Form1();
            Application.EnableVisualStyles();
            if (form.Show() == DialogResult.OK)
            {
                Console.WriteLine("Running visual");
            }
            else
            {
                form.ready = false;
                form.thread.Interrupt();
                Environment.Exit(0);
            }
        }
    }
}
