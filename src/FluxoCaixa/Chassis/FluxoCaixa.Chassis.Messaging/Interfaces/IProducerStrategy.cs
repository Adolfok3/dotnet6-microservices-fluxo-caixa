using FluxoCaixa.Chassis.Messaging.Enum;

namespace FluxoCaixa.Chassis.Messaging.Interfaces
{
    internal interface IProducerStrategy
    {
        IProducerClient GetProducer(Broker broker);
    }
}
