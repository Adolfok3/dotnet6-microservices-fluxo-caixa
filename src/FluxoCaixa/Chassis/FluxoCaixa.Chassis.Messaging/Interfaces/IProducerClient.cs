using FluxoCaixa.Chassis.Messaging.Enum;

namespace FluxoCaixa.Chassis.Messaging.Interfaces
{
    internal interface IProducerClient
    {
        Task SendAsync(string service, string consumer, string data);
        Broker GetBroker();
    }
}
