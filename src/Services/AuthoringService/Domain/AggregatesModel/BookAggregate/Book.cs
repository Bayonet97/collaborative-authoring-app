using CA.Services.AuthoringService.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate
{
    public class Book
    {
        public Guid Id { get; protected set; }

        private Guid ownerId;
        private List<Guid> collaboratorIds;
        private string bookTitle;
        private LinkedList<Page> pages;


        /// <summary>
        /// Gets and sets the user id of the kweet.
        /// </summary>
        public Guid OwnerId
        {
            get => ownerId;
            private set
            {
                if (value == Guid.Empty)
                    throw new BookDomainException("The owner id is empty.");
                ownerId = value;
            }
        }

        /// <summary>
        /// Gets a read only set of pages.
        /// </summary>
        public virtual IReadOnlyCollection<Page> Pages => pages;

        internal Page AddPage()
        {
            Page page = new(this);
            if (Pages.Contains(page))
                return default;
            pages.AddLast(page);
            return page;
        }

        internal Page RemovePage()
        {
            Page page = Pages.LastOrDefault(page => page.Paragraphs.Count() > 0);

            if (page == default)
                return default;
            bool removed = pages.Remove(page);
            //if (removed)
               // AddDomainEvent(new KweetUnlikedDomainEvent(Id, userId));
            return page;
        }
    }
}
