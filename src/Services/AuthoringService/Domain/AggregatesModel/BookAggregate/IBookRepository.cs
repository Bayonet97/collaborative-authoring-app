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
        Task<bool> CreateAsync(Book book);

        public Task<List<Book>> FindAllByUserId(Guid userId);
        
        ValueTask<Book> FindAsync(Guid bookId, CancellationToken cancellation);

        ValueTask<bool> UpdateAsync(Book book);

        List<Page> UpdatePages(List<Page> pages);

        Page UpdatePage(Page page);

        Task<bool> AddCollaborator(Guid bookId, Guid userId);
    }
}
