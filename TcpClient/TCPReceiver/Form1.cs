using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPReceiver
{ 

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Thread thread = new Thread(new ThreadStart(Listen));
            thread.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void Listen()
        {

            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            int port = 9001;

            TcpListener tcpListener = new TcpListener(localAddr, port);

            tcpListener.Start();

            while (true)

            {

                TcpClient tcpClient = tcpListener.AcceptTcpClient();//接受挂起的tcp请求

                NetworkStream ns = tcpClient.GetStream();

                StreamReader sr = new StreamReader(ns);

                string result = sr.ReadToEnd();

                //显示读取的字符串

                Invoke(new UpdateDisplayDelegate(UpdateDisplay), new object[] { result });

                //Action<string> aa =new Action<string>(UpdateDisplay);
                //aa.Invoke(result);

            }

        }

        public void UpdateDisplay(string text)
        {
            this.richTextBox1.Text = text;
            //txtDisplay.Text = text;
        }

        protected delegate void UpdateDisplayDelegate(string text);
    }
}
