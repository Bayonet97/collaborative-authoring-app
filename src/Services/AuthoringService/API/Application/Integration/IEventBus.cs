using CA.Services.AuthoringService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Integration
{
    /// <summary>
    /// Represents the <see cref="IEventBus"/> interface.
    /// </summary>
    public interface IEventBus : IEventPublisher
    {
        /// <summary>
        /// Subscribes to the event bus using a specified queue name.
        /// </summary>
        /// <param name="queueName">The name of the queue to subscribe to.</param>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <typeparam name="TEventHandler">The type of event handler.</typeparam>
        public void Subscribe<TEvent, TEventHandler>(string queueName) where TEvent : Event where TEventHandler : IEventHandler<TEvent>;

        /// <summary>
        /// Unsubscribe a queue from the event bus.
        /// </summary>
        /// <param name="queueName">The name of the queue to unsubscribe from.</param>
        public void Unsubscribe(string queueName);
    }
}
