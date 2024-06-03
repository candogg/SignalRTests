using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalR.Shared
{
    public class NotificationClient
    {
        private readonly Action<string> messageCallback;
        private readonly Action<string> connectionIdReceivedCallback;
        private HubConnection connection;

        public NotificationClient(Action<string> messageCallback, Action<string> connectionIdReceivedCallback)
        {
            this.messageCallback = messageCallback;
            this.connectionIdReceivedCallback = connectionIdReceivedCallback;
        }

        public async Task StartAsync()
        {
            await StopAsync();

            string url = "https://localhost:7217/notificationhub";

            connection = new HubConnectionBuilder()
           .WithUrl(url)
           .Build();

            connection.On<string>("ReceiveConnectionId", connectionId =>
            {
                connectionIdReceivedCallback?.Invoke(connectionId);
            });

            connection.On<string>("ReceiveNotification", update =>
            {
                messageCallback?.Invoke(update);
            });

            await connection.StartAsync();
        }

        public async Task StopAsync()
        {
            if (connection == null || connection.State == HubConnectionState.Disconnected) return;

            await connection.StopAsync();

            await connection.DisposeAsync();
        }
    }
}
