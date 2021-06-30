using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.RemarkService.Domain.AggregatesModel.BookAggregate
{
    public interface IBookRepository
    {
        Book Create(Book book);

        Book Update(Book book);

        ValueTask<Book> FindAsync(Guid bookId, CancellationToken cancellationToken);

        Remark CreateRemark(Remark remark);

        ValueTask<Remark> UpdateRemarkAsync(Remark remark);

        ValueTask<Remark> FindRemarkByBookAsync(Guid remarkId, Guid bookId, CancellationToken cancellationToken);

        ValueTask<IEnumerable<Remark>> FindAllRemarksInBookAsync(Guid bookId, CancellationToken cancellationToken);
    }
}
