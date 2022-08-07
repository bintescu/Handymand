using System.Threading.Tasks;

namespace Handymand.Controllers.Chat
{
    public interface IChatHub
    {
        Task MessageReceivedFromHub(ChatMessage message);

        Task NewUserConnected(string message);
    }
}
