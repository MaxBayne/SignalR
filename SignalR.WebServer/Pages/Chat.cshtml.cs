using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using SignalR.WebServer.Hubs;

namespace SignalR.WebServer.Pages
{
    public class ChatModel : PageModel
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHubContext;

        public ChatModel(IHubContext<ChatHub, IChatClient> chatHubContext)
        {
            _chatHubContext = chatHubContext;
        }

        public void OnGet()
        {
            
        }

        public async void OnPost()
        {
            await _chatHubContext.Clients.All.ReceiveMessage("RazorPage(WebServer)", "hello from razor page call hub method outside hub class");
        }
    }
}
