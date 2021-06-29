using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.Events
{
    public abstract class DomainEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent"/> class.
        /// </summary>
        protected DomainEvent()
        {
            EventId = Guid.NewGuid();
            EventName = GetType().Name;
            EventCreationDateTime = DateTime.UtcNow;
        }
    }
}
