using Kafka.Public;
using Kafka.Public.Loggers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Threading.Tasks;

namespace CA.Services.RemarkService.API.Kafka.Consumers
{
    public class KafkaConsumerBase
    {
        protected ClusterClient _cluster;
        public KafkaConsumerBase()
        {
            _cluster = new ClusterClient(new Configuration
            {
                // Set to the message broker's domain
                Seeds = "10.72.3.221:9092",
                OffsetOutOfRangeStrategy = Offset.Earliest,
                ErrorStrategy = ErrorStrategy.Retry,
                OverflowStrategy = OverflowStrategy.Block,

            }, new ConsoleLogger()) ;
        }

        /// <inheritdoc cref="IEventSerializer.Deserialize{TMessage}(ReadOnlyMemory{byte})"/>
        public TMessage Deserialize<TMessage>(ReadOnlyMemory<byte> message) where TMessage : class
        {
            return JsonSerializer.Deserialize<TMessage>(message.Span, new() { PropertyNameCaseInsensitive = true});
        }
    }
}
