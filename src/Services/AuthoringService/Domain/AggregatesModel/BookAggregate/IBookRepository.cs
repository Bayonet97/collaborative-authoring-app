using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate
{
    public interface IBookRepository
    {
        Book Create(Book book);

        public List<Book> FindAllByUserId(Guid userId);
        
        ValueTask<Book> FindAsync(Guid bookId, CancellationToken cancellation);

        Book Update(Book book);

        List<Page> UpdatePages(List<Page> pages);

        Page UpdatePage(Page page);
    }
}
