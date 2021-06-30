using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using CA.Services.AuthoringService.API.Middleware;
using System.Threading;
using MediatR;
using CA.Services.AuthoringService.API.Application.Commands.ChangePageCommand;
using CA.Services.AuthoringService.API.Application.Commands;

namespace CA.Services.AuthoringService.API.Controllers
{
    public class AuthoringHub : Hub
    {
        public static Dictionary<Guid, string> BookGroups = new Dictionary<Guid, string>();
        private readonly IMediator mediator;

        public AuthoringHub(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task Connect(string connectionJson)
        {
            //Guid userId = Guid.Parse(Context.User.Claims.Single(claim => claim.Type == "UserId").Value);

            Connection connection = JsonConvert.DeserializeObject<Connection>(connectionJson);
            if (!BookGroups.ContainsKey(connection.BookId))
            {
                BookGroups.Add(connection.BookId, connection.BookId.ToString());
            }
            BookGroups.TryGetValue(connection.BookId, out string bookId);
            await Groups.AddToGroupAsync(Context.ConnectionId, bookId);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("---> Connection Established" + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceivedConnectionId", Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }

        public async Task SendMessageAsync(string pageChangesJson)
        {
            //TODO: Replace dynamic with concrete class
            ChangePageCommand changePageCommand = JsonConvert.DeserializeObject<ChangePageCommand>(pageChangesJson);

            CommandResponse commandResponse = await mediator.Send(changePageCommand);
            Console.WriteLine("Message Received on: " + Context.ConnectionId);
            if (!commandResponse.Success)
            {
                await Clients.Caller.SendAsync("BookChanged", "Failed to save changes");
            }
            await Clients.Group(changePageCommand.BookId.ToString()).SendAsync("BookChanged", pageChangesJson);
        }
    }
}
