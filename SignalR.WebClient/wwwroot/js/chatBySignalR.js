
var txtHubUrl = document.getElementById("txtHubUrl");
var btnConnect = document.getElementById("btnConnect");
var btnDisconnect = document.getElementById("btnDisConnect");
var btnJoinGroup = document.getElementById("btnJoinGroup");
var btnLeaveGroup = document.getElementById("btnLeaveGroup");
var btnSend = document.getElementById("btnSend");
var btnSendObject = document.getElementById("btnSendObject");
var btnSendToGroup = document.getElementById("btnSendToGroup");
var btnGetAllUsers = document.getElementById("btnGetAllUsers");
var txtUser = document.getElementById("txtUser");


//Build Connection To Server Hub and Enable Logging inside Client Side
var connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Debug)
    //.withUrl("https://localhost:7032/hubs/chatHub", signalR.HttpTransportType.LongPolling)
    //.withUrl("https://localhost:7032/hubs/chatHub", signalR.HttpTransportType.ServerSentEvents)
    //.withUrl("https://localhost:7032/hubs/chatHub", signalR.HttpTransportType.WebSockets)
    .withUrl(txtHubUrl.value) //its default to websockets
    .build();

//Register Events Handlers For Client Methods when Server try to ask to execute method

//Client will wait if server ask to call methods inside it
connection.on("ReceiveMessage", function (sender, message) {
    var li = document.createElement("li");

    document.getElementById("messagesList").appendChild(li);

    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    li.textContent = `${sender} says ${message}`;
});

connection.on("ReceiveMessageAsObject", function (msgObj)
{
    var li = document.createElement("li");

    document.getElementById("messagesList").appendChild(li);

    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    li.textContent = `${msgObj.user} says ${msgObj.content}`;
});

//Client will wait if server ask to call methods inside it
connection.on("ReceiveMessageFromGroup", function (group, message) {
    var li = document.createElement("li");

    document.getElementById("messagesList").appendChild(li);

    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    li.textContent = `${group} Group => says ${message}`;
});

//Client will wait if server ask to call methods inside it
connection.on("UpdateTotalUsers", function (totalUsers) {
    var lblTotalUsers = document.getElementById("lblTotalUsers");

    lblTotalUsers.innerText = totalUsers;
});



//Connect to Hub Server via WebSocket
btnConnect.onclick = function ()
{
    try
    {
        connection.start().then(() =>
        {
            window.$("#txtConnectionId").val(connection.connectionId);

            //Ask Server about name
            connection.invoke("GetServerName").then((value) =>
            {
                alert(value);
            });

            //Ask Server To Add Current Connection to this User
            connection.invoke("AddCurrentConnectionToUser", txtUser.value).then(() =>
            {
                alert("Added Connection To User " + txtUser.value);
            });
        });


    }
    catch (e)
    {
        console.error(e);
    } 

    
};

//Disconnect from Hub Server via WebSocket
btnDisConnect.onclick = function ()
{
    try 
    {
        connection.stop().then(() =>
        {
            window.$("#txtConnectionId").val("-");
        });    
    }
    catch (e) 
    {
        console.error(e);
    } 
    
};



//Join Client To Group
btnJoinGroup.onclick = function ()
{
    try 
    {
        var group = document.getElementById("txtGroup").value;

        connection.invoke("JoinClientToGroup", connection.connectionId, group)
            .then(() =>
            {
                alert("Client Join the Group");
            });

    } catch (e) 
    {
        alert("Cant Join to the Group : " + e.toString());
        console.error(e);
    } 
    


};

//Leave Client From Group
btnLeaveGroup.onclick = function ()
{
    try {
        var group = document.getElementById("txtGroup").value;

        connection.invoke("RemoveClientFromGroup", connection.Id, group)
            .then(() =>
            {
                alert("Client Leave the Group");
            });    

    } catch (e) 
    {
        alert("Cant Leave the Group : " + e.toString());
        console.error(e);
    } 
    
};



//Send Message to Hub Server
btnSend.onclick=function (event)
{
    try {
        var sender = document.getElementById("txtUser").value;
        var message = document.getElementById("txtMessage").value;

        //Client will call method called (SendMessage) that defined inside Server Hub and pass parameters
        connection.invoke("SendMessage", sender, message)
            .then(() => {
                alert("Send Message To All Clients");
            });
            

        event.preventDefault();    
    }
    catch (e) 
    {
        console.error(e);

    } 

    
};

//Send Object to Hub Server
btnSendObject.onclick = function (event)
{
    try
    {
        var user = document.getElementById("txtUser").value;
        var message = document.getElementById("txtMessage").value;

        //Message class
        connection.invoke("SendMessageAsObject", { User: user, Content: message })
            .then(() =>
            {
                alert("Send Object To All Clients");
            });


        event.preventDefault();
    }
    catch (e) 
    {
        console.error(e);

    }


};

//Send Message to specific Group into Hub Server 
btnSendToGroup.onclick = function (event)
{
    try
    {
        var message = document.getElementById("txtMessage").value;
        var group = document.getElementById("txtGroup").value;

        //Client will call method called (SendMessageToClientsInsideGroup) that defined inside Server Hub and pass parameters
        connection.invoke("SendMessageToClientsInsideGroup", group, message)
            .then(() => 
            {
                alert("Send Message To Group");
            });
            
        event.preventDefault();
        
    } catch (e) {
        console.error(e);
    } 
    

};

btnGetAllUsers.onclick = function (event)
{
    try {
        //Ask Server To Get All Users
        connection.invoke("GetAllUsers")
            .then((usersArray) => {
                for (let i = 0; i < usersArray.length; i++) {
                    console.log("ConnectionId:" +
                        usersArray[i].connectionId +
                        " - " +
                        "Name:" +
                        usersArray[i].name +
                        " - " +
                        "Email:" +
                        usersArray[i].email);
                }

                event.preventDefault();
            });
    } catch (e) 
    {
        console.error(e);
    } 
    
       

    
};
