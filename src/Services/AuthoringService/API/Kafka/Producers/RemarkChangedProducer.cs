using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate.Events;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Kafka.Producers
{
    public class RemarkChangedProducer : KafkaProducerBase, IHostedService
    {
        private IProducer<Null, string> _producer;

        public RemarkChangedProducer() : base()
        {

        }

        public async Task ProduceAsync(object sender, Remark remark)
        {
         
            // Creates a message and pushes to kafka with topic 'message'
            await _producer.ProduceAsync("RemarkChanged", new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(remark)
            }, CancellationToken.None);

            // Make sure the queue is empty, time out at 10 seconds.
            _producer.Flush(TimeSpan.FromSeconds(10));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a producer with given types and config. 1st type param is key, 2nd is value.
            _producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
            RemarkChangedDomainEvent.RemarkChangedEvent += async (s, r) => await ProduceAsync(s, r);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Destroy the producer when done.
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
