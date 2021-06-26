using CA.Services.AuthoringService.Domain.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate
{
    public class Page
    {
        private Guid bookId;
        private ConcurrentDictionary<short, Paragraph> paragraphs;

        public Guid BookId
        {
            get => bookId;
            private set
            {
                if(value == Guid.Empty)
                {
                    throw new BookDomainException("The book id is empty.");
                }
            }
        }
        public virtual IReadOnlyDictionary<short, Paragraph> Paragraphs => paragraphs;

        public int ParagraphCount => paragraphs.Count;

        protected Page() { }

        public Page(Book book)
        {
            bookId = book.Id;
        }
        
        public async Task<Paragraph> AddParagraph(Guid paragraphId, short position)
        {
            if(paragraphId == Guid.Empty)
            {
                throw new BookDomainException("Paragraph id is empty.");
            }
            if(paragraphs.Count == 0 && position > 0)
            {
                position = 0;
            } else if(paragraphs.Count <= position)
            {
                position = Convert.ToInt16(paragraphs.Count - 1);
            }

            Paragraph paragraph = new(paragraphId);


            //await MoveParagraphPositions(position);

            paragraphs.TryAdd(position, paragraph);

            return paragraph;
        }

/*        private Task MoveParagraphPositions(short newParagraphPosition)
        {
            foreach(short position in paragraphs.Keys)
            {
                if(position >= newParagraphPosition)
                {
                    paragraphs.AddOrUpdate(position, )
                }
            }
        }*/

        public bool RemoveParagraph(short paragraphPosition)
        {
            if (paragraphPosition >= paragraphs.Count)
            {
                throw new BookDomainException("Paragraph to remove doesn't exist.");
            }

            bool success = paragraphs.Remove(paragraphPosition, out _);

            return success;
        }
    }
}
