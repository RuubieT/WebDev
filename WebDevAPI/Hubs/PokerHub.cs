using Microsoft.AspNetCore.SignalR;

namespace WebDevAPI.Hubs
{
    public class PokerHub: Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
