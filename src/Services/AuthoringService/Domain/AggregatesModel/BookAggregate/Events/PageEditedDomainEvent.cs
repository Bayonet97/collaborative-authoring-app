using CA.Services.AuthoringService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate.Events
{
    class PageEditedDomainEvent : DomainEvent
    {
        public Page Page { get; }

        public PageEditedDomainEvent(Page page)
        {
            Page = page;
        }
    }
}
