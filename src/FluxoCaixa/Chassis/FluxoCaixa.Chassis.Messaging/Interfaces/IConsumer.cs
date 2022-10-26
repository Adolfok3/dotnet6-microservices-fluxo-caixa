namespace FluxoCaixa.Chassis.Messaging.Interfaces
{
    public interface IConsumer
    {
        Task OnExecuting(string data);
        string GetKey();
    }
}
