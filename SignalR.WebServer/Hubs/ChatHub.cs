using Microsoft.AspNetCore.SignalR;
using System.Reflection;

namespace SignalR.WebServer.Hubs
{
    /*
     *Server Hub Can Display Information about the Client from there
     * -------------------------------------------------------------
     *  Context.ConnectionId
     *
     * Server Hub Method can Share data over hub methods for every client connection using collection items (key/value) pairs
     *  Context.Items["ClientId"]
     *  Context.Items["ClientName"]
     *  Context.Items["ClientAddress"]
     *
     * Server Can abort the connection of client by using
     *  Context.Abort();
     *
     * Server Can Call Method for the Caller of the hub method to execute method inside this client side
     *  Clients.Caller
     *
     * Server can Call Method for other caller expected the client who call the method to execute method inside clients side
     *  Clients.Others
     *
     * Server can call Method for specific client by connection id
     *  Clients.Client(connectionId)
     *
     * Server can call Method for specific group by group name so any client inside this group will call its client side method
     *  Clients.Group("GroupName")
     *
     * you can change the name of hub method using this attribute [HubMethodName]
     *
     */

    /// <summary>
    /// We will Define the Methods defined inside Client side on this interface to be Strongly typed hubs
    /// </summary>
    public interface IChatClient
    {
        //Client receive message from server
        Task ReceiveMessage(string sender,string message);

        //Client receive message from server group
        Task ReceiveMessageFromGroup(string group,string message);


        //Client receive message from server and need to response to it
        Task<string> GetMessage();

        //Client receive message from server and need to response to it
        Task<string> GetClientName();
    }

    /// <summary>
    /// Hub Manage Communications between clients and server
    /// </summary>
    public class ChatHub:Hub<IChatClient>
    {
        #region Connection Life Cycle

        //Every Time Client Connected to Server Hub
        public override Task OnConnectedAsync()
        {
            PrintClientInfo();
            return base.OnConnectedAsync();
        }

        //Every Time Client Disconnected from Server Hub
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("Client Disconnected : " + exception?.Message);
            return base.OnDisconnectedAsync(exception);
        }

        #endregion

        #region Hub Methods

        //Any Client Can Call this method by MethodName and Parameters
        public async Task SendMessage(string sender, string message) => await Clients.All.ReceiveMessage(sender, message);

        //Server Will Send Message to all Clients
        public async Task SendMessageToAllClients(string sender, string message) => await Clients.All.ReceiveMessage(sender, message);

        //Server will Send Message to Client who call method
        public async Task SendMessageToCallerClient(string sender, string message) => await Clients.Caller.ReceiveMessage(sender, message);

        //Server Will Send Message to all Clients inside group
        public async Task SendMessageToClientsInsideGroup(string group,string message) => await Clients.Group(group).ReceiveMessageFromGroup(group, message);





        //server ask client to response to message by execute method inside client side and return to response
        public async Task<string> GetMessageFromClient(string connectionId)
        {
            var message = await Clients.Client(connectionId).GetMessage();

            Console.WriteLine($"Get Message Result From Client with Connection ({connectionId}) with Result ({message})");

            return message;

        }

        [HubMethodName("Get_Client_Name")]
        public async Task<string> GetClientName(string connectionId)
        {
            var message = await Clients.Client(connectionId).GetClientName();
            
            Console.WriteLine($"Get Client Name Result From Client with Connection ({connectionId}) with Result ({message})");

            return message;
        }

        #endregion

        #region Groups Methods

        public async Task JoinClientToGroup(string clientConnectionId, string groupName)
        {
            //Add Client Connection To Group
            await Groups.AddToGroupAsync(clientConnectionId, groupName);

            string message = $"Client with id ({clientConnectionId}) Joined Group ({groupName})";

            //Notify Clients Group that new Client Join to that Group
            await Clients.Groups(groupName).ReceiveMessage("Hub", message);

            Console.ForegroundColor=ConsoleColor.DarkBlue;
            Console.WriteLine(message);

        }
        
        public async Task RemoveClientFromGroup(string clientConnectionId, string groupName)
        {
            //Remove Client Connection From Group
            await Groups.RemoveFromGroupAsync(clientConnectionId, groupName);

            string message = $"Client with id ({clientConnectionId}) Remove from Group ({groupName})";

            //Notify Clients Group that new Client Join to that Group
            await Clients.Groups(groupName).ReceiveMessage("Hub", message);

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(message);
        }

        #endregion

        #region Helper

        private void PrintClientInfo()
        {
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine($"New Client with Id ({Context.ConnectionId}) Connected to Hub");
            if (Context.Items.Any())
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Items Collections Data");
                Console.WriteLine("---------------------------------------------------------------------------------------");
                foreach (var item in Context.Items)
                {
                    Console.WriteLine($"Item Key ({item.Key}) has value ({item.Value})");
                }
                Console.WriteLine("---------------------------------------------------------------------------------------");
            }
            
        }

        #endregion
    
    }
}
