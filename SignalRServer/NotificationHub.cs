using Microsoft.AspNetCore.SignalR;

namespace SignalRServer
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string connectionId, string message)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            await Clients.Caller.SendAsync("ReceiveConnectionId", connectionId);
            await base.OnConnectedAsync();
        }
    }
}
