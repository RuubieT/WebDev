using Microsoft.AspNetCore.SignalR;

namespace WebDevAPI.Hubs
{
    public class PokerHub: Hub
    {
        [HubMethodName("start_connection")]
        public void StartConnection(string message)
        {
            Clients.All.SendAsync("sendMessage", message);
        }
    }
}
