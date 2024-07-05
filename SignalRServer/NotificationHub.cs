using Microsoft.AspNetCore.SignalR;

namespace SignalRServer
{
    public class NotificationHub(ChatService chatService) : Hub
    {
        private readonly ChatService chatService = chatService;

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveConnectionId", Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var disconnectedUser = await chatService.RemoveUserAsync(Context.ConnectionId);

            await Clients.Others.SendAsync("UserDisconnected", disconnectedUser);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
