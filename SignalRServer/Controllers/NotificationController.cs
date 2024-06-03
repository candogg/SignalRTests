using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalRServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(IHubContext<NotificationHub> hubContext) : ControllerBase
    {
        private readonly IHubContext<NotificationHub> hubContext = hubContext;

        [HttpPost("SendNotification")]
        public async Task<IActionResult> SendNotification(MessageItem messageItem)
        {
            await hubContext.Clients.Client(messageItem.ConnectionId).SendAsync("ReceiveNotification", messageItem.Message);
            return Ok();
        }
    }
}
