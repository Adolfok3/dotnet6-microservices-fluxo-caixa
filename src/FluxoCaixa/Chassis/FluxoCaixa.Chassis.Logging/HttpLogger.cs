using FluxoCaixa.Chassis.Logging.Interfaces;
using FluxoCaixa.Chassis.Logging.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
using ILogger = Serilog.ILogger;

namespace FluxoCaixa.Chassis.Logging
{
    public class HttpLogger : IHttpLogger
    {
        private readonly ILogger _logger;

        public HttpLogger(IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetValue<string>("MongoConnectionString");
            _logger = new LoggerConfiguration()
                .WriteTo.MongoDB(mongoConnectionString, "http")
                .CreateLogger();
        }

        public void Log(HttpLog log)
        {
            _logger
                .ForContext("Application", log.Application)
                .ForContext("RequestUrl", log.RequestUrl)
                .ForContext("RequestMethod", log.RequestMethod)
                .ForContext("RequestHeaders", log.RequestHeaders)
                .ForContext("RequestBody", log.RequestBody)
                .ForContext("ResponseBody", log.ResponseBody)
                .ForContext("ResponseDate", log.ResponseDate)
                .ForContext("StatusCode", log.StatusCode)
                .Information("Http log");
        }
    }
}
