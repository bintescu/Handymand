
using Handymand.Controllers.Chat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpPost("sendmessage")]
        public async Task SendMessage(ChatMessage message)
        {
            await this.hubContext.Clients.All.SendAsync("messageReceivedFromApi", message);
        }

        [HttpGet("notifications")] 
        public async Task SendNotifications()
        {
            await this.hubContext.Clients.All.SendAsync("updateNotification");
        }
    }
}
