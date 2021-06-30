using CA.Services.RemarkService.API.Application.Commands;
using CA.Services.RemarkService.API.Application.Commands.UpdateRemarkCommand;
using CA.Services.RemarkService.Domain.AggregatesModel.BookAggregate;
using MediatR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.RemarkService.API.Kafka.Consumers
{
    public class RemarkChangedConsumer : KafkaConsumerBase, IHostedService
    {
        private readonly IMediator _mediator;
        public RemarkChangedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Consume messages if there are any of topic 'message'
            _cluster.ConsumeFromLatest("RemarkChanged");
            // Subscribe to MessageReceived from the Kafka library. Some magic here
            _cluster.MessageReceived += async (record) =>
            {
                Remark r = Deserialize<Remark>(record.Value as byte[]);

                UpdateRemarkCommand updateRemarkCommand = new() 
                { 
                    RemarkId = r.Id, BookId = r.BookId, PageId = r.PageId, Text = r.RemarkedText, StartPosition = r.StartPosition, EndPosition = r.EndPosition 
                };

                CommandResponse commandResponse = await _mediator.Send(updateRemarkCommand);
            };
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cluster?.Dispose();
            return Task.CompletedTask;
        }
    }
}
