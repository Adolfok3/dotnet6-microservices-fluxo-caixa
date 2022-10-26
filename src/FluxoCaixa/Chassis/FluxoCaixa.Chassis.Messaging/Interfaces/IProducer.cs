namespace FluxoCaixa.Chassis.Messaging.Interfaces
{
    public interface IProducer
    {
        Task SendAsync(string service, string consumer, string data);
    }
}
