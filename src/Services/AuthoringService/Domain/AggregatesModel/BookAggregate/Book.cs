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
        private List<Guid> collaboratorIds = new();
        private string bookTitle;
        private LinkedList<Page> pages = new();


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

        public virtual IReadOnlyCollection<Guid> CollaboratorIds => collaboratorIds;

        public string BookTitle
        {
            get => bookTitle;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new BookDomainException("The book title is null, empty or contains only whitespaces.");
                bookTitle = value;
            }
        }
        /// <summary>
        /// Gets a read only set of pages.
        /// </summary>
        public virtual IReadOnlyCollection<Page> Pages => pages;

        public Book()
        {

        }

        public Book(Guid bookId, Guid owner, string title, Guid firstPageId)
        {
            Id = bookId;
            OwnerId = owner;
            BookTitle = title;
            AddPage(firstPageId);
        }

        public bool AddCollaborator(Guid collaboratorId)
        {
            if(collaboratorId == Guid.Empty) 
            {
                throw new BookDomainException("The collaborator id is empty.");
            }
            else if (collaboratorIds.Contains(collaboratorId))
            {
                return default;
            }

            collaboratorIds.Add(collaboratorId);
            return true;
        }

        public bool UpdatePage(Page page)
        {
            pages.Find(page).Value = page;

            return true;
        }

        public Page AddPage(Guid pageId)
        {
            Page page = new(pageId, this);
            if (Pages.Contains(page))
                return default;
            pages.AddLast(page);
            return page;
        }

        public Page RemovePage()
        {
            Page page = Pages.LastOrDefault();

            if (page == default)
                return default;
            bool removed = pages.Remove(page);
            //if (removed)
               // AddDomainEvent(new KweetUnlikedDomainEvent(Id, userId));
            return page;
        }
    }
}
