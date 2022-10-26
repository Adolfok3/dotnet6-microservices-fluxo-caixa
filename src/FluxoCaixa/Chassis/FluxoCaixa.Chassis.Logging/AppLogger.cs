using System.Text.Json;
using FluxoCaixa.Chassis.Logging.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace FluxoCaixa.Chassis.Logging
{
    public class AppLogger : IAppLogger
    {
        private readonly ILogger _logger;

        public AppLogger(IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetValue<string>("MongoConnectionString");
            _logger = new LoggerConfiguration()
                .WriteTo.MongoDB(mongoConnectionString, "logs")
                .CreateLogger();
        }

        public void Log(string message, object data = null)
        {
            message = data != null ? message+" - {@data}" : message;
            _logger.Write(LogEventLevel.Information, message, JsonSerializer.Serialize(data));
        }
    }
}
