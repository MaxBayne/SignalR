using Microsoft.AspNetCore.SignalR;

namespace SignalR.WebServer.Hubs
{
    /// <summary>
    /// Hub Manage Communications between clients and server
    /// </summary>
    public class ChatHub:Hub
    {
        //Just Call Method (ReceiveMessage) and pass Parameters inside All Clients
        public async Task SendMessage(string sender, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", sender, message);
        }
    }
}
