using FluxoCaixa.Chassis.Messaging.Enum;
using FluxoCaixa.Chassis.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FluxoCaixa.Chassis.Messaging
{
    internal class Producer : IProducer
    {
        private readonly IProducerClient _producerClient;

        public Producer(IProducerStrategy producerStrategy, IConfiguration configuration)
        {
            var broker = configuration.GetValue<Broker>("Broker");
            _producerClient = producerStrategy.GetProducer(broker);
        }

        public async Task SendAsync(string service, string consumer, string data)
        {
            await _producerClient.SendAsync(service, consumer, data);
        }
    }
}
