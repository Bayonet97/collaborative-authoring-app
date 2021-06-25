using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate
{
    public class Page
    {
        private Guid bookId;
        private ConcurrentDictionary<short, Paragraph> paragraphs;

        public virtual IReadOnlyDictionary<short, Paragraph> Paragraphs => paragraphs;

        protected Page() { }

        public Page(Book book)
        {
            bookId = book.Id;
        }
    }
}
