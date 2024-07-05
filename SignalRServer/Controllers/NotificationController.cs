using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project.Shared.Items;

namespace SignalRServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(IHubContext<NotificationHub> hubContext, ChatService chatService) : ControllerBase
    {
        private readonly IHubContext<NotificationHub> hubContext = hubContext;
        private readonly ChatService chatService = chatService;

        [HttpPost("SendNotification")]
        public async Task<IActionResult> SendNotification(MessageItem messageItem)
        {
            await hubContext.Clients.Client(messageItem.ConnectionId).SendAsync("ReceiveNotification", messageItem);

            return Ok();
        }

        [HttpPost("SetOnline")]
        public async Task<IActionResult> SetOnline(ChatUser userItem)
        {
            await chatService.AddUserAsync(userItem);

            await hubContext.Clients.AllExcept(userItem.ConnectionId).SendAsync("UserConnected", userItem);

            return Ok();
        }

        [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers(ChatUser userItem)
        {
            var allUsers = await chatService.GetUsersAsync(userItem);

            return Ok(allUsers);
        }
    }
}
