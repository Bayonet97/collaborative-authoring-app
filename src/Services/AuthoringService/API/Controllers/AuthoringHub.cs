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
namespace CA.Services.AuthoringService.API.Controllers
{
    public class AuthoringHub : Hub
    {
        public static Dictionary<Book, string> BookGroup = new Dictionary<Book, string>();
        private readonly IBookRepository bookRepository;

        public AuthoringHub(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public async Task Connect(string connectionJson)
        {
            //Guid userId = Guid.Parse(Context.User.Claims.Single(claim => claim.Type == "UserId").Value);

            Connection connection = JsonConvert.DeserializeObject<Connection>(connectionJson);
            Book book = BookGroup.Keys.Where(b => b.Id == connection.BookId).FirstOrDefault();
            if (book == null)
            {
                book = await bookRepository.FindAsync(connection.BookId, CancellationToken.None);
                BookGroup.Add(book, book.Id.ToString());
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, book.Id.ToString());
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
            PageChanges pageChanges = JsonConvert.DeserializeObject<PageChanges>(pageChangesJson);

            
            if(pageChanges.PageChangeType == PageChangeType.ADDITION)
            {

            }
            Console.WriteLine("Message Received on: " + Context.ConnectionId);

            await Clients.Group(pageChanges.BookId.ToString()).SendAsync("BookChanged", pageChangesJson);
        }
    }
}
