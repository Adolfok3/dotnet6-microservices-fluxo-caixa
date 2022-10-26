namespace FluxoCaixa.Chassis.Logging.Interfaces
{
    public interface IAppLogger
    {
        void Log(string message, object data = null);
    }
}
