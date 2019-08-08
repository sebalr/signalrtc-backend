using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace signalRtc.hubs
{
    public class SignalRtcHub : Hub
    {

        public async Task NewPeer(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.OthersInGroup(group).SendAsync("NewPeer", Context.ConnectionId);
        }

        public async Task SendSignal(string signal, string user)
        {
            await Clients.Client(user).SendAsync("SendSignal", Context.ConnectionId, signal);
        }
    }
}
