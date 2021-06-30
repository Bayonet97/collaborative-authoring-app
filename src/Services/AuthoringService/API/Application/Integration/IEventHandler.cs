using CA.Services.AuthoringService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Integration
{
    /// <summary>
    /// Represents the <see cref="IEventHandler{TEvent}"/> interface.
    /// </summary>
    /// <typeparam name="TEvent">The type of event.</typeparam>
    public interface IEventHandler<in TEvent> where TEvent : Event
    {
        /// <summary>
        /// The unsubscribe event handler.
        /// </summary>
        public event EventHandler UnsubscribeEventHandler;

        /// <summary>
        /// Unsubscribes from the event bus.
        /// </summary>
        public void Unsubscribe();

        /// <summary>
        /// Handles the event asynchronously.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="ValueTask"/>.</returns>
        public ValueTask HandleAsync(TEvent @event, CancellationToken cancellationToken);
    }
}
