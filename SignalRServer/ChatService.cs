using Project.Shared.Items;
using System.Collections.Concurrent;

namespace SignalRServer
{
    public class ChatService
    {
        private readonly ConcurrentDictionary<string, ChatUser> activeUsers;

        public ChatService()
        {
            activeUsers = new ConcurrentDictionary<string, ChatUser>();
        }

        public async Task<ChatUser> AddUserAsync(ChatUser chatUser)
        {
            if (activeUsers.ContainsKey(chatUser.ConnectionId))
            {
                activeUsers.TryRemove(chatUser.ConnectionId, out _);
            }

            activeUsers.TryAdd(chatUser.ConnectionId, chatUser);

            return await Task.FromResult(chatUser);
        }

        public async Task<List<ChatUser>> GetUsersAsync(ChatUser chatUser)
        {
            return await Task.FromResult(activeUsers.Select(x => x.Value).Where(a => a.ConnectionId != chatUser.ConnectionId).ToList());
        }

        public async Task<ChatUser?> RemoveUserAsync(string connectionId)
        {
            if (activeUsers.ContainsKey(connectionId))
            {
                activeUsers.TryRemove(connectionId, out var chatUser);

                return await Task.FromResult(chatUser);
            }

            return await Task.FromResult<ChatUser?>(null);
        }
    }
}
