using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Controllers.Chat
{
    public class ChatHub: Hub<IChatHub>
    {
        public async Task BroadcastAsync(ChatMessage message)
        {
            await Clients.All.MessageReceivedFromHub(message);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.NewUserConnected("a new user connectd");
        }
    }
}
