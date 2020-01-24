using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using signalRtc.models;

namespace signalRtc.hubs
{
    public class SignalRtcHub : Hub
    {

        public async Task NewUser(string username)
        {
            var userInfo = new UserInfo() { userName = username, connectionId = Context.ConnectionId };
            await Clients.Others.SendAsync("NewUser", JsonSerializer.Serialize(userInfo));
        }

        public async Task HelloUser(string userName, string user)
        {
            var userInfo = new UserInfo() { userName = userName, connectionId = Context.ConnectionId };
            await Clients.Client(user).SendAsync("HelloUser", JsonSerializer.Serialize(userInfo));
        }

        public async Task SendSignal(string signal, string user)
        {
            await Clients.Client(user).SendAsync("SendSignal", Context.ConnectionId, signal);
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await Clients.All.SendAsync("UserDisconnect", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
