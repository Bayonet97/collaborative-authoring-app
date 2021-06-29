using CA.Services.AuthoringService.API.Application.Integration;
using CA.Services.AuthoringService.API.Kafka.Producers;
using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.DomainEventHandlers
{
    public sealed class RemarkChangedDomainEventHandler
    {
        private readonly RemarkChangedProducer remarkChangedProducer;

        public RemarkChangedDomainEventHandler(RemarkChangedProducer remarkChangedProducer)
        {
            this.remarkChangedProducer = remarkChangedProducer;
        }

        public Task Handle()
        {
            throw new NotImplementedException();
        }
    }
}
