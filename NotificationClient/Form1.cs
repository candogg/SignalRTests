using System;
using System.Windows.Forms;

namespace NotificationClient
{
    public partial class Form1 : Form
    {
        private readonly NotificationClient notificationClient;

        public Form1()
        {
            InitializeComponent();

            notificationClient = new NotificationClient(MessageCallback, ConnectionIdReceivedCallback);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            await notificationClient.Start();
        }

        private void MessageCallback(string message)
        {
            Invoke((MethodInvoker)delegate
            {
                listBox1.Items.Insert(0, message);
            });
        }

        private void ConnectionIdReceivedCallback(string connectionId)
        {
            Invoke((MethodInvoker)delegate
            {
                textBox1.Text = connectionId;
            });
        }
    }
}
