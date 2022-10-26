using FluxoCaixa.Chassis.Messaging.Enum;
using FluxoCaixa.Chassis.Messaging.Interfaces;

namespace FluxoCaixa.Chassis.Messaging.Configuration
{
    internal class ProducerStrategy : IProducerStrategy
    {
        private readonly IEnumerable<IProducerClient> _producerClients;

        public ProducerStrategy(IEnumerable<IProducerClient> producers)
        {
            _producerClients = producers;
        }

        public IProducerClient GetProducer(Broker broker)
        {
            var producer = _producerClients.FirstOrDefault(f => f.GetBroker().Equals(broker));
            return producer ?? throw new ArgumentException($"Broker {broker} was not configured.");
        }
    }
}
