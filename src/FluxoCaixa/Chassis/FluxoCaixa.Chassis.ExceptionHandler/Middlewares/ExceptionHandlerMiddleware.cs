using FluxoCaixa.Chassis.ExceptionHandler.Dictionaries.Interfaces;
using FluxoCaixa.Chassis.ExceptionHandler.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using FluxoCaixa.Chassis.Logging.Interfaces;

namespace FluxoCaixa.Chassis.ExceptionHandler.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandlerStatusCodeDictionary _dictionary;
        private readonly IErrorLogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, IExceptionHandlerStatusCodeDictionary dictionary, IErrorLogger logger)
        {
            _next = next;
            _dictionary = dictionary;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)GetStatusCode(error.GetType());

                LogError(error, response.StatusCode);

                await response.WriteAsJsonAsync(new ErrorResponse(error.Message));
            }
        }

        private void LogError(Exception error, int statusCode)
        {
            if (statusCode == 500)
            {
                _logger.Fatal(error);
                return;
            }

            _logger.Error(error);
        }

        private HttpStatusCode GetStatusCode(Type error)
        {
            if (!_dictionary.TryGetValue(error, out var statusCode))
                statusCode = HttpStatusCode.InternalServerError;

            return statusCode;
        }
    }
}
