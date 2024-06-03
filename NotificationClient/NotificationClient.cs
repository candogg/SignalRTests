using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace NotificationClient
{
    public class NotificationClient
    {
        private readonly Action<string> messageCallback;
        private readonly Action<string> connectionIdReceivedCallback;

        public NotificationClient(Action<string> messageCallback, Action<string> connectionIdReceivedCallback)
        {
            this.messageCallback = messageCallback;
            this.connectionIdReceivedCallback = connectionIdReceivedCallback;
        }

        public async Task Start()
        {
            string url = "https://localhost:7217/notificationhub";

            var hubConnection = new HubConnectionBuilder()
           .WithUrl(url)
           .Build();

            hubConnection.On<string>("ReceiveConnectionId", connectionId =>
            {
                connectionIdReceivedCallback?.Invoke(connectionId);
            });

            hubConnection.On<string>("ReceiveNotification", update =>
            {
                messageCallback?.Invoke(update);
            });

            await hubConnection.StartAsync();
        }
    }
}
