using FluxoCaixa.Chassis.Logging.Models;

namespace FluxoCaixa.Chassis.Logging.Interfaces
{
    public interface IHttpLogger
    {
        void Log(HttpLog log);
    }
}
