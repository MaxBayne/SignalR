using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace SignalR.WindowsFormsClient
{
    public partial class FrmChat : Form
    {
        private HubConnection _connection;
        

        #region Form

        public FrmChat()
        {
            InitializeComponent();
        }

        

        private void frmChat_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region Connection Methods

        private async void connectButton_Click(object sender, EventArgs e)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(addressTextBox.Text)
                .ConfigureLogging(configureLogging => configureLogging.SetMinimumLevel(LogLevel.Debug))
                .WithAutomaticReconnect() //if client lost connection with server then client will try reconnect after 0,2,10,30 seconds and last if fail to reconnect then will wire closed event
                .Build();

            
            _connection.Reconnecting += (exception) =>
            {
                MessageBox.Show($@"Connection To Server Lost and Try Reconnecting Error={exception}");
                return Task.CompletedTask;
            };
            _connection.Reconnected += (connectionId) =>
            {
                MessageBox.Show($@"Connection To Server Come back after Lost with ConnectionId={connectionId}");
                return Task.CompletedTask;
            };
            _connection.Closed += async(exception) =>
            {
                MessageBox.Show(@"Closed Connection , please try to manual connect again");

                //Try to ReConnect after delay 10 second
                await Task.Delay(10000);
                await _connection.StartAsync();
                //return await Task.CompletedTask;
            };


            //Client will wait for any server called to call methods defined inside Client side
            _connection.On<string, string>("ReceiveMessage", (name, message) =>
            {
                Log(Color.Black, name + ": " + message);
            });

            _connection.On<string, string>("ReceiveMessageFromGroup", (group, message) =>
            {
                Log(Color.Black, group + "Group: " + message);
            });

            _connection.On("GetMessage", () =>
            {
                return Task.FromResult("Message sent from client desktop app");
            });

            _connection.On("Get_Client_Name", () =>
            {
                return Task.FromResult("Iam Desktop Client");
            });

            _connection.On<int>("UpdateTotalUsers", (totalUsers) =>
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.Text = $@"Chat - (TotalUsers = {totalUsers})";
                    }));
                }
                else
                {
                    this.Text = $@"Chat - (TotalUsers = {totalUsers})";
                }

            });



            Log(Color.Gray, "Starting connection...");

            await _connection.StartAsync();

            Log(Color.Gray, "Connection established.");

            messageTextBox.Focus();
        }

        private async void disconnectButton_Click(object sender, EventArgs e)
        {
            Log(Color.Gray, "Stopping connection...");

            await _connection.StopAsync();

            this.Text = @"Chat";

            Log(Color.Gray, "Connection terminated.");
        }

        #endregion

        #region Groups Methods

        private async void btn_JoinGroup_Click(object sender, EventArgs e)
        {
            await _connection.InvokeAsync("JoinClientToGroup", _connection.ConnectionId, txt_GroupName.Text);
            Log(Color.Blue, $"Joined With Group {txt_GroupName.Text}");
            
        }

        private async void btn_LeaveGroup_Click(object sender, EventArgs e)
        {
            await _connection.InvokeAsync("RemoveClientFromGroup", _connection.ConnectionId, txt_GroupName.Text);
            Log(Color.Blue, $"Leaving Group {txt_GroupName.Text}");
        }

        #endregion

        #region Messages Methods

        private async void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Client will call method called (SendMessage) that defined inside Server Hub and pass parameters to it
                await _connection.InvokeAsync("SendMessage", txt_sender.Text, messageTextBox.Text);
            }
            catch (Exception ex)
            {
                Log(Color.Red, ex.ToString());
            }
        }

        private async void btn_SendToGroup_Click(object sender, EventArgs e)
        {
            try
            {
                //Client will call method called (SendMessage) that defined inside Server Hub and pass parameters to it
                await _connection.InvokeAsync("SendMessageToClientsInsideGroup", txt_GroupName.Text, messageTextBox.Text);
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
            AcceptButton = btn_Connect;
        }

        private void messageTextBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = btn_SendToAll;
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
