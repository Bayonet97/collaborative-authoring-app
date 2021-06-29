using CA.Services.AuthoringService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate.Events
{
    public static class RemarkChangedDomainEvent
    {
        public static Remark Remark { get; }

        public static EventHandler<Remark> RemarkChangedEvent;

        public static void RemarkChanged(Remark remark)
        {
            RemarkChangedEvent?.Invoke(remark, remark);
        }
    }
}
