using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Internship.Web
{
    public class EchoHub : Hub
    {
        public string UserId => Context.UserIdentifier;

        public async Task ToastMaster(string message)
        {
            await Clients.All.SendAsync("ClientMasterMessage", message);
        }

        public async Task SendUserStatus(string message)
        {
            await Clients.User(UserId).SendAsync("ClientStatus", message);
        }
    }
}