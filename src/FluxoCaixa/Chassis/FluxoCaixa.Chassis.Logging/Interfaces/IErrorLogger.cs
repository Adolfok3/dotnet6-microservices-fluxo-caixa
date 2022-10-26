namespace FluxoCaixa.Chassis.Logging.Interfaces
{
    public interface IErrorLogger
    {
        void Error(Exception e, string description = "Exception caught through exception middleware");
        void Fatal(Exception e, string description = "Exception caught through exception middleware");
    }
}
