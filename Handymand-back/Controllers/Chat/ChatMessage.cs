using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Controllers.Chat
{
    public class ChatMessage
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string ConnectionId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
