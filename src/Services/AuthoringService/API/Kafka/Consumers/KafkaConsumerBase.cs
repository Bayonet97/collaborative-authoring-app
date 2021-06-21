using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Kafka.Consumers
{
    public abstract class KafkaConsumerBase
    {
        protected readonly ConsumerConfig _consumerConfig;

        public KafkaConsumerBase()
        {
            _consumerConfig = new ConsumerConfig()
            {
                // Set to the message broker's domain
                BootstrapServers = "localhost:29092"
            };
        }
        public abstract Task StartAsync(CancellationToken cancellationToken);

        public abstract Task StopAsync(CancellationToken cancellationToken);
    }
}
