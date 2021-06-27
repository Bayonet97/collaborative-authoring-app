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
        public Guid Id;
        private Guid bookId;
        private string text;
        private List<Remark> remarks;

        public string Text 
        { 
            get => text;
        }

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

        public List<Remark> Remarks
        {
            get => remarks;
        }

        protected Page() { }

        public Page(Guid id, Book book)
        {
            Id = id;
            bookId = book.Id;
        }
  
        public string AddText(string letters, int position)
        {
            if (String.IsNullOrEmpty(letters))
            {
                return default;
            }

            if(position >= Text.Length)
            {
                position = Text.Length - 1;
            }

            text.Insert(position, letters);

            // Fire text edited event.

            return Text;
        }

        public string RemoveText(int amount, int position)
        {
            if(amount == 0)
            {
                return default;
            }

            if(position >= Text.Length)
            {
                return default;
            }

            text.Remove(position, amount);

            // Fire text edited event.

            return Text;
        }

        public Remark AddRemark(Guid id, string remarkedText, int[,] position, Page page)
        {
            Remark r = new(id, remarkedText, position, page);

            return r;
        }

        public bool RemoveRemark(Guid id)
        {
            Remark r = remarks.Find(r => r.Id == id);

            if(r == null)
            {
                throw new BookDomainException("Remark doesn't exist.");
            }

            bool success =  remarks.Remove(r);

            return success;
        }
    }
}
