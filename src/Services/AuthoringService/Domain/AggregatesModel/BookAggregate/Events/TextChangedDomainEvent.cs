using CA.Services.AuthoringService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate.Events
{
    public class TextChangedDomainEvent : Event
    {
        public Page Page { get; }
        public string NewText { get; }
        public int Position { get; }
        public int? RemovedAmount { get; }

        public TextChangedDomainEvent(Page page, string newText, int position, int? removedAmount)
        {
            Page = page;
            NewText = newText;
            Position = position;
            RemovedAmount = removedAmount;
        }
    }
}
