using CA.Services.AuthoringService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate.Events
{
    public class RemarkAddedDomainEvent : Event
    {
        public Remark Remark { get; }

        public RemarkAddedDomainEvent(Remark remark)
        {
            Remark = remark;
        }
    }
}
