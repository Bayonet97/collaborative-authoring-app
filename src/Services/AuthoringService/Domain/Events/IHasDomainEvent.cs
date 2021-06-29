using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.Events
{
    public interface IHasDomainEvent
    {
        public List<Event> DomainEvents { get; set; }
    }
}
