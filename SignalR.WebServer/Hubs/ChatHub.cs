using Microsoft.AspNetCore.SignalR;
using SignalR.WebServer.Models;
using SignalR.WebServer.Services;
using SignalR.WebServer.HubsFilters;

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
     *  Group is collection of connections
     *
     * you can change the name of hub method using this attribute [HubMethodName]
     *
     * Multi Connections can Related to One User like client user can have multi connections like (web app , desktop app, mobile app) so this connections can be related to one user
     * and when server send message to this user so all connections related to that user will receive the message
     *
     */

    /// <summary>
    /// We will Define the Methods defined inside Client side on this interface to be Strongly typed hubs
    /// </summary>
    public interface IChatClient
    {
        //Client receive message from server
        Task ReceiveMessage(string sender,string message);

        //Client receive message object from server
        Task ReceiveMessageAsObject(Message message);

        //Client receive message from server group
        Task ReceiveMessageFromGroup(string group,string message);

        Task UpdateTotalUsers(int totalUsers);


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
        private readonly IDatabaseService _databaseService;

        public ChatHub(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        #region Connection Life Cycle

        public static int TotalUsers { get; set; }

        //Every Time Client Connected to Server Hub
        public override Task OnConnectedAsync()
        {
            PrintClientInfo();

            TotalUsers++;

            //Notify All Users about new connection
            Clients.All.UpdateTotalUsers(TotalUsers).GetAwaiter().GetResult();

            //Store Connected User to database Service
            _databaseService.AddUser(new UserModel(){ConnectionId = Context.ConnectionId,Name = Context.UserIdentifier!});

            return base.OnConnectedAsync();
        }

        //Every Time Client Disconnected from Server Hub
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;

            //Notify All Users about leave connection
            Clients.All.UpdateTotalUsers(TotalUsers).GetAwaiter().GetResult();

            //Remove the Disconnected User From Store Database
            _databaseService.RemoveUser(Context.ConnectionId);

            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("Client Disconnected : " + exception?.Message);
            return base.OnDisconnectedAsync(exception);
        }

        #endregion

        #region Hub Methods

        //Any Client Can Call this method by MethodName and Parameters
        public async Task SendMessage(string sender, string message) => await Clients.All.ReceiveMessage(sender, message);


        //Any Client Can Call this method by MethodName and Parameters
        public async Task SendMessageAsObject(Message msg) => await Clients.All.ReceiveMessageAsObject(msg);

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

        public Task<string> GetServerName()
        {
            return Task.FromResult(@"Iam SignalR Server");
        }

        public Task<IEnumerable<UserModel>> GetAllUsers(IDatabaseService databaseService)
        {
            return Task.FromResult<IEnumerable<UserModel>>(databaseService.Users.Values.ToList());
        }

        #endregion

        #region Users Methods

        public async Task AddCurrentConnectionToUser(string user)
        {
            
        }

        //Send Message to Specific User so all Connections related to that user will receive the message
        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User(user).ReceiveMessage(user, message);
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

        public async Task JoinCurrentConnectionToGroup(string groupName)
        {
            //Add Client Connection To Group
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            string message = $"Client with id ({Context.ConnectionId}) Joined Group ({groupName})";

            //Notify Clients Group that new Client Join to that Group
            await Clients.Groups(groupName).ReceiveMessage("Hub", message);

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(message);

        }

        public async Task RemoveCurrentConnectionFromGroup(string groupName)
        {
            //Remove Client Connection From Group
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            string message = $"Client with id ({Context.ConnectionId}) Remove from Group ({groupName})";

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
            Console.WriteLine($"New Connection with Id ({Context.ConnectionId}) Connected to Hub");
            Console.WriteLine($"New Connection Related to User: ({Context.UserIdentifier})");
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

    public class Message
    {
        public string User { get; set; }
        public string Content { get; set; }
    }
}
