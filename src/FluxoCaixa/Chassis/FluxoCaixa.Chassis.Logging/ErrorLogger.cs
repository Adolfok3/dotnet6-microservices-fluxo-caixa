using FluxoCaixa.Chassis.Logging.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using ILogger = Serilog.ILogger;

namespace FluxoCaixa.Chassis.Logging
{
    public class ErrorLogger : IErrorLogger
    {
        private readonly ILogger _logger;

        public ErrorLogger(IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetValue<string>("MongoConnectionString");
            _logger = new LoggerConfiguration()
                .WriteTo.MongoDB(mongoConnectionString, "errors")
                .CreateLogger();
        }

        public void Error(Exception e, string description = "Exception caught through exception middleware")
        {
            _logger.Error(e, description);
        }

        public void Fatal(Exception e, string description = "Exception caught through exception middleware")
        { 
            _logger.Fatal(e, description);
        }
    }
}
