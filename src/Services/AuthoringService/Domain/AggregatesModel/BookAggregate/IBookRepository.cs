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

        ValueTask<Page> FindPageAsync(Guid pageId, Guid bookId);
        List<Page> UpdatePages(List<Page> pages);

        ValueTask<Page> UpdatePage(Page page, string letters, int position);

        Task<bool> AddCollaborator(Guid bookId, Guid userId);

        Task<bool> CheckBookOwner(Guid userId, Guid bookId);
        ValueTask<bool> UpdateRemarkAsync(Guid bookId, Guid pageId, Remark remark);
        Task<bool> CheckCollaborator(Guid userId, Guid bookId);
    }
}
