using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Infrastructure.Repositories
{
    public sealed class BookRepository : IBookRepository
    {
        private BookDbContext _bookDbContext;

        private SynchronizedCollection<Book> books = new();

        public BookRepository(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext ?? throw new ArgumentNullException(nameof(bookDbContext));
        }
        public BookRepository()
        {
            User owner = new(Guid.Parse("6ac6cd64-0a3f-41ad-a8a0-de5c1b9bf5e3"), "BookOwner");
            User collaborator = new(Guid.Parse("6ac6cd64-0a3f-41ad-a8a0-de5c1b9bf5e4"), "BookCollaborator");
    
            books.Add(new Book(Guid.Parse("910a9dff-7b21-46c6-9e80-090793cf1d0f"), owner.Id, "Lord of the Cars"));
            books[0].AddCollaborator(collaborator.Id);

            Console.WriteLine($"Owner: {owner.Id}, Collaborator: {collaborator.Id}, Page: {books[0].Pages.First().Id}");

        }

        public async Task<bool> CreateAsync(Book book)
        {
           await Task.Run(() => books.Add(book));

            return true;
        }

        public async Task<List<Book>> FindAllByUserId(Guid userId)
        {
            List<Book> b = await Task.Run(() => books.Where(b => b.OwnerId == userId || b.CollaboratorIds.Contains(userId)).ToList());

            return b;
        }

        public async ValueTask<Book> FindAsync(Guid bookId, CancellationToken cancellation)
        {
            Book b = await Task.Run(() => books.Single(b => b.Id == bookId));

            return b;
        }

        public async ValueTask<bool> UpdateAsync(Book book)
        {
            int index = books.IndexOf(books.Single(b => b.Id == book.Id));
            await Task.Run(() => books[index] = book);
            if(books[index].Id != book.Id)
            {
                return false;
            }
            return true;
        }

        public Page UpdatePage(Page page)
        {
            throw new NotImplementedException();
        }

        public List<Page> UpdatePages(List<Page> pages)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddCollaborator(Guid bookId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
