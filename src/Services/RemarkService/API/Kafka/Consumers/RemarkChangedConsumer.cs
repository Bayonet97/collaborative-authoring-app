using AutoMapper;
using CA.Services.RemarkService.Domain.AggregatesModel.UserAggregate;
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
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Consume messages if there are any of topic 'message'
            _cluster.ConsumeFromLatest("RemarkChanged");
            // Subscribe to MessageReceived from the Kafka library. Some magic here
            _cluster.MessageReceived += record =>
            {
                Remark r = Deserialize<Remark>(record.Value as byte[]);
                dynamic recObject = record.Value;
                //Remark r = Mapper.Map<Remark>(record.Value);
                // Process each received record, received as a byte array
                Console.WriteLine(r);
            };
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
