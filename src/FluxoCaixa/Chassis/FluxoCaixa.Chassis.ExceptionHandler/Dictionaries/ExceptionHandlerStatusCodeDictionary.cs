using FluxoCaixa.Chassis.ExceptionHandler.Dictionaries.Interfaces;
using System.Net;

namespace FluxoCaixa.Chassis.ExceptionHandler.Dictionaries
{
    public class ExceptionHandlerStatusCodeDictionary : Dictionary<Type, HttpStatusCode>, IExceptionHandlerStatusCodeDictionary
    {
        public ExceptionHandlerStatusCodeDictionary()
        {
            Add(typeof(ArgumentException), HttpStatusCode.BadRequest);
            Add(typeof(ArgumentNullException), HttpStatusCode.BadRequest);
            Add(typeof(ArgumentOutOfRangeException), HttpStatusCode.BadRequest);
            Add(typeof(UnauthorizedAccessException), HttpStatusCode.Unauthorized);
        }
    }
}
