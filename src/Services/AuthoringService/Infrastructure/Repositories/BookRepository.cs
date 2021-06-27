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
            User owner = new(Guid.NewGuid(), "BookOwner");
            User collaborator = new(Guid.NewGuid(), "BookCollaborator");
    
            books.Add(new Book(Guid.NewGuid(), owner.Id, "Lord of the Cars"));
            books[0].AddCollaborator(collaborator.Id);
            books[0].AddPage();

            Console.WriteLine($"Owner: {owner.Id}, Collaborator: {collaborator.Id}");

        }

        public Book Create(Book book)
        {
            books.Add(book);

            return book;
        }

        public List<Book> FindAllByUserId(Guid userId)
        {
            List<Book> b = books.Where(b => b.OwnerId == userId || b.CollaboratorIds.Contains(userId)).ToList();

            return b;
        }

        public ValueTask<Book> FindAsync(Guid bookId, CancellationToken cancellation)
        {
            Book b = books.Single(b => b.Id == bookId);
            ValueTask<Book> v = new ValueTask<Book>(b);

            return v;
        }

        public Book Update(Book book)
        {
            throw new NotImplementedException();
        }

        public Page UpdatePage(Page page)
        {
            throw new NotImplementedException();
        }

        public List<Page> UpdatePages(List<Page> pages)
        {
            throw new NotImplementedException();
        }
    }
}
