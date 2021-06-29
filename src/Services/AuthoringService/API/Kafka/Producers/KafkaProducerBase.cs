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
    public abstract class KafkaProducerBase
    {
        protected readonly ProducerConfig _producerConfig;

        public KafkaProducerBase()
        {
            _producerConfig = new ProducerConfig()
            {
                // Set to the message broker's domain
                BootstrapServers = "localhost:29092",
                BrokerAddressFamily = BrokerAddressFamily.V4
            };
        }
    }
}
