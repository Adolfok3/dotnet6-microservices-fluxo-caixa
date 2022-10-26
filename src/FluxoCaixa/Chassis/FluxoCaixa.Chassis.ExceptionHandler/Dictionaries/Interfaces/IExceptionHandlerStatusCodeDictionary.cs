using System.Net;

namespace FluxoCaixa.Chassis.ExceptionHandler.Dictionaries.Interfaces
{
    public interface IExceptionHandlerStatusCodeDictionary : IDictionary<Type, HttpStatusCode>
    {
    }
}
