using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Kafka.Producers
{
    public class PageUpdatedProducer : KafkaProducerBase
    {
        private readonly ILogger<PageUpdatedProducer> _logger;
        private IProducer<Null, string> _producer;

        public PageUpdatedProducer(ILogger<PageUpdatedProducer> logger) : base()
        {
            _logger = logger;
            // Create a producer with given types and config. 1st type param is key, 2nd is value.
            _producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // This is test code that runs on start
            for (int i = 0; i < 10; i++)
            {
                var value = $"This is input from a client, input number: {i}";
                _logger.LogInformation(value);
                // Creates a message and pushes to kafka with topic 'message'
                await _producer.ProduceAsync("message", new Message<Null, string>()
                {
                    Value = value
                }, cancellationToken);
            }
            // Make sure the queue is empty, time out at 10 seconds.
            _producer.Flush(TimeSpan.FromSeconds(10));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Destroy the producer when done.
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
