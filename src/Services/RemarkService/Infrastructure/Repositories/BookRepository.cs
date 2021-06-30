using CA.Services.RemarkService.Domain.AggregatesModel.BookAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.RemarkService.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private BookDbContext _bookDbContext;

        private SynchronizedCollection<Book> books = new();

        public BookRepository(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext ?? throw new ArgumentNullException(nameof(bookDbContext));
        }

        public BookRepository()
        {
            //User remarker = new(Guid.Parse("6ac6cd64-0a3f-41ad-a8a0-de5c1b9bf5e3"), "BookOwner");
            Book book = new(Guid.Parse("910a9dff-7b21-46c6-9e80-090793cf1d0f"));
            Remark remark = new(Guid.Parse("9557f61d-a085-45fd-b678-0f3c1ac39192"), "Hello.", (1,5), Guid.Parse("c74f8e17-40ab-441b-aa27-6bd8dde8b61a"), Guid.Parse("910a9dff-7b21-46c6-9e80-090793cf1d0f"));
            remark.RemarkString = "This text is remarked.";

            book.Remarks.Add(remark);

            books.Add(book);
        }
        public Book Create(Book book)
        {
            throw new NotImplementedException();
        }

        public Remark CreateRemark(Remark remark)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<Remark>> FindAllRemarksInBookAsync(Guid bookId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Book> FindAsync(Guid bookId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Remark> FindRemarkByBookAsync(Guid remarkId, Guid bookId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Book Update(Book book)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<Remark> UpdateRemarkAsync(Remark remark)
        {
            int index = books.IndexOf(books.Single(b => b.Id == remark.BookId));
            await Task.Run(() => books[index].UpdateRemarkPosition(remark));

            return books[index].Remarks.Find(r => r.Id == remark.Id);
        }
    }
}
