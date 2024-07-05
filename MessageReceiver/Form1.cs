using System;
using System.Windows.Forms;

namespace MessageReceiver
{
    public partial class Form1 : Form
    {
        private readonly NotificationClient notificationClient;

        public Form1()
        {
            InitializeComponent();

            notificationClient = new NotificationClient(MessageCallback, ConnectionIdReceivedCallback);
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

        private async void Form1_Load(object sender, EventArgs e)
        {
            await notificationClient.StartAsync();

            textBox1.Text = string.Empty;
            listBox1.Items.Clear();
        }
    }
}
