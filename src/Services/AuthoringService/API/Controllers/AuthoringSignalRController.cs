using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CA.Services.AuthoringService.API.Controllers
{
    public class AuthoringSignalRController : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("---> Connection Established" + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceivedConnectionId", Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public async Task SendMessageAsync(string message)
        {
            //TODO: Replace dynamic with concrete class
            var route = JsonConvert.DeserializeObject<dynamic>(message);
            string toClient = route.To;
            Console.WriteLine("Message Received on: " + Context.ConnectionId);

            if(toClient == string.Empty)
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            else
            {
                await Clients.Client(toClient).SendAsync("ReceiveMessage", message);
            }
        }
    }
}
