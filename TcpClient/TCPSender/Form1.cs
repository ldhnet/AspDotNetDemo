using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPSender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            var filePath = "c:\\temp\\tasklog.txt";
            txtHost.Text = "127.0.0.1";
            txtPort.Text = string.IsNullOrEmpty(txtPort.Text) ? "9001" : txtPort.Text;
            TcpClient tcpClient = new TcpClient(txtHost.Text, Int32.Parse(txtPort.Text));

            NetworkStream ns = tcpClient.GetStream();

            FileStream fs = File.Open(filePath, FileMode.Open);

            int data = fs.ReadByte();

            while (data != -1)

            {

                ns.WriteByte((byte)data);

                data = fs.ReadByte();

            }

            fs.Close();

            ns.Close();

            tcpClient.Close();
        }

        private void Send2_Click(object sender, EventArgs e)
        {
            var test1 = this.richTextBox1.Text;

            txtHost.Text = "127.0.0.1";
            txtPort.Text = string.IsNullOrEmpty(txtPort.Text) ? "9001" : txtPort.Text;

            byte[] decBytes = Encoding.UTF8.GetBytes(test1);

            TcpClient tcpClient = new TcpClient(txtHost.Text, Int32.Parse(txtPort.Text));

            NetworkStream ns = tcpClient.GetStream();

            foreach (var item in decBytes)
            {
                ns.WriteByte(item);
            }
            ns.Close();

            tcpClient.Close();
        }
        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }
    }
}
