using Microsoft.AspNetCore.SignalR;

namespace WebDevAPI.Hubs
{
    public class ChatHub: Hub
    {
        public static int TotalViews { get; set; } = 0;

        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
        }
    }
}
