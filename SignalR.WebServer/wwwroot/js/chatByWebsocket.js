var txtWebSocketUrl = document.getElementById("txtWebSocketUrl");
var txtConnectionId = document.getElementById("txtConnectionId");
var btnConnect = document.getElementById("btnConnect");
var btnDisconnect = document.getElementById("btnDisConnect");
var btnJoinGroup = document.getElementById("btnJoinGroup");
var btnLeaveGroup = document.getElementById("btnLeaveGroup");
var btnSend = document.getElementById("btnSend");
var btnSendToGroup = document.getElementById("btnSendToGroup");


//Connect to Hub Server via WebSocket
btnConnect.onclick = function () 
{
    var socket = new WebSocket(txtWebSocketUrl.value);

    socket.onopen = function ()
    {
        //txtConnectionId.val(socket.id);

        alert("WebSocket Connection Opened");
    };

    socket.onclose = function ()
    {
        //txtConnectionId.val("-");

        alert("WebSocket Connection Closed");
    };

    socket.onmessage = function (event)
    {
        alert(event.data);
    };

    socket.onerror = function (event) {
        console.log(event);
    }
};

//Disconnect from Hub Server via WebSocket
btnDisConnect.onclick = function ()
{

};

//Send Message to Hub Server
btnSend.onclick = function ()
{
    /*
    var sender = document.getElementById("txtSender").value;
    var message = document.getElementById("txtMessage").value;

    //Client will call method called (SendMessage) that defined inside Server Hub and pass parameters
    connection.invoke("SendMessage", sender, message)
        .then(() =>
        {
            alert("Send Message To All Clients");
        })
        .catch(function (err)
        {
            return console.error(err.toString());
        });

    event.preventDefault();
    */
};

//Send Message to specific Group into Hub Server
btnSendToGroup.onclick = function ()
{
    /*
    var message = document.getElementById("txtMessage").value;
    var group = document.getElementById("txtGroup").value;

    //Client will call method called (SendMessageToClientsInsideGroup) that defined inside Server Hub and pass parameters
    connection.invoke("SendMessageToClientsInsideGroup", group, message)
        .then(() =>
        {
            alert("Send Message To Group");
        })
        .catch(function (err)
        {
            return console.error(err.toString());
        });

    event.preventDefault();
    */
};

/*
//Client will wait if server ask to call methods inside it
connection.on("ReceiveMessage", function (sender, message) {
    var li = document.createElement("li");

    document.getElementById("messagesList").appendChild(li);

    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    li.textContent = `${sender} says ${message}`;
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

*/