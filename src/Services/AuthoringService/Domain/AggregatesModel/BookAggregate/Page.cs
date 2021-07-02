using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate.Events;
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
        public event EventHandler<TextChangedDomainEvent> TextChangedEvent;
        public event EventHandler<RemarkAddedDomainEvent> RemarkAddedDomainEvent;
        public Guid Id;
        private Guid bookId;
        private string text;
        private SynchronizedCollection<Remark> remarks = new SynchronizedCollection<Remark>();

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

        public SynchronizedCollection<Remark> Remarks
        {
            get => remarks;
        }

        protected Page() { }

        public Page(Guid id, Book book)
        {
            Id = id;
            bookId = book.Id;
            text = String.Empty;
        }
  
        public string AddText(string letters, int position)
        {
            if (String.IsNullOrEmpty(letters))
            {
                return default;
            }

            if(position >= Text.Length)
            {
                if (Text.Length == 0)
                {
                    position = 0;
                }
                else
                {
                    position = Text.Length - 1;
                }
            }

            text = text.Insert(position, letters);

            TextChangedEvent?.Invoke(this, new(this, letters, position, 0));

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

            TextChangedEvent?.Invoke(this, new(this, String.Empty, position, amount));

            return Text;
        }

        public Remark AddRemark(Remark remark)
        {
            remarks.Add(remark);
            RemarkAddedDomainEvent?.Invoke(this, new(remark));
            return remark;
        }

        public bool RemoveRemark(Guid id)
        {
            Remark r = remarks.First(r => r.Id == id);

            if(r == null)
            {
                throw new BookDomainException("Remark doesn't exist.");
            }

            bool success =  remarks.Remove(r);

            return success;
        }
    }
}
