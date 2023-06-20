using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR.WindowsFormsClient
{
    public partial class FrmChat : Form
    {
        private HubConnection _connection;

        public FrmChat()
        {
            InitializeComponent();
        }


        private void frmChat_Load(object sender, EventArgs e)
        {
            addressTextBox.Focus();
        }


        #region Button

        private async void connectButton_Click(object sender, EventArgs e)
        {
            UpdateState(connected: false);

            _connection = new HubConnectionBuilder()
                .WithUrl(addressTextBox.Text)
                .Build();

            _connection.On<string, string>("ReceiveMessage", (name, message) =>
            {
                Log(Color.Black, name + ": " + message);
            });

            Log(Color.Gray, "Starting connection...");
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Log(Color.Red, ex.ToString());
                return;
            }

            Log(Color.Gray, "Connection established.");

            UpdateState(connected: true);

            messageTextBox.Focus();
        }

        private async void disconnectButton_Click(object sender, EventArgs e)
        {
            Log(Color.Gray, "Stopping connection...");
            try
            {
                await _connection.StopAsync();
            }
            catch (Exception ex)
            {
                Log(Color.Red, ex.ToString());
            }

            Log(Color.Gray, "Connection terminated.");

            UpdateState(connected: false);
        }

        private async void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                await _connection.InvokeAsync("SendMessage", txt_sender.Text, messageTextBox.Text);
            }
            catch (Exception ex)
            {
                Log(Color.Red, ex.ToString());
            }
        }
        #endregion

        #region TextBox
        private void addressTextBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = connectButton;
        }

        private void messageTextBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = sendButton;
        }

        #endregion

        #region List

        private void messagesList_DrawItem(object sender, DrawItemEventArgs e)
        {
            var message = (LogMessage)messagesList.Items[e.Index];
            e.Graphics.DrawString(
                message.Content,
                messagesList.Font,
                new SolidBrush(message.MessageColor),
                e.Bounds);
        }

        #endregion





        #region Helper

        private void UpdateState(bool connected)
        {
            disconnectButton.Enabled = connected;
            connectButton.Enabled = !connected;
            addressTextBox.Enabled = !connected;

            messageTextBox.Enabled = connected;
            sendButton.Enabled = connected;
        }

        private void Log(Color color, string message)
        {
            Action callback = () =>
            {
                messagesList.Items.Add(new LogMessage(color, message));
            };

            Invoke(callback);
        }

        private class LogMessage
        {
            public Color MessageColor { get; }

            public string Content { get; }

            public LogMessage(Color messageColor, string content)
            {
                MessageColor = messageColor;
                Content = content;
            }
        }

        #endregion




    }
}
