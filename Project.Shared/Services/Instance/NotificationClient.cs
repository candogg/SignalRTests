using Microsoft.AspNetCore.SignalR.Client;
using Project.Shared.Items;

namespace Project.Shared.Services.Instance
{
    public class NotificationClient
    {
        private readonly Action<MessageItem> messageCallback;
        private readonly Action<string> connectionIdReceivedCallback;
        private readonly Action<ChatUser> userConnectedCallback;
        private readonly Action<ChatUser> userDisconnectedCallback;
        private HubConnection? connection;
        private readonly string url;

        public NotificationClient(Action<MessageItem> messageCallback, Action<string> connectionIdReceivedCallback, Action<ChatUser> userConnectedCallback, Action<ChatUser> userDisconnectedCallback)
        {
            url = "https://localhost:7217/notificationhub";

            this.messageCallback = messageCallback;
            this.connectionIdReceivedCallback = connectionIdReceivedCallback;
            this.userConnectedCallback = userConnectedCallback;
            this.userDisconnectedCallback = userDisconnectedCallback;
        }

        public async Task StartAsync()
        {
            if (connection != null)
            {
                await connection.StopAsync();
                await connection.DisposeAsync();
            }

            connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            connection.On<string>("ReceiveConnectionId", connectionId =>
            {
                connectionIdReceivedCallback?.Invoke(connectionId);
            });

            connection.On<MessageItem>("ReceiveNotification", update =>
            {
                messageCallback?.Invoke(update);
            });

            connection.On<ChatUser>("UserConnected", update =>
            {
                userConnectedCallback?.Invoke(update);
            });

            connection.On<ChatUser>("UserDisconnected", update =>
            {
                userDisconnectedCallback?.Invoke(update);
            });

            connection.Closed += Connection_Closed;

            await connection.StartAsync();
        }

        private Task Connection_Closed(Exception? arg)
        {
            return Task.CompletedTask;
        }

        public async Task StopAsync()
        {
            if (connection == null || connection.State == HubConnectionState.Disconnected) return;

            await connection.StopAsync();

            await connection.DisposeAsync();
        }
    }
}
