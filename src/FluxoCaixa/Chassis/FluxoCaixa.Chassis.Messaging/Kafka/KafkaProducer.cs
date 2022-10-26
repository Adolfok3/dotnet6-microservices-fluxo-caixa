using Confluent.Kafka;
using FluxoCaixa.Chassis.Logging.Interfaces;
using FluxoCaixa.Chassis.Messaging.Enum;
using FluxoCaixa.Chassis.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FluxoCaixa.Chassis.Messaging.Kafka
{
    internal class KafkaProducer : IProducerClient
    {
        private readonly IErrorLogger _logger;
        private readonly ProducerConfig _config;

        public KafkaProducer(IConfiguration configuration, IErrorLogger logger)
        {
            _logger = logger;
            _config = new ProducerConfig
            {
                BootstrapServers = configuration.GetValue<string>("KafkaAddress"),
                MessageTimeoutMs = 5000
            };
        }

        public async Task SendAsync(string service, string consumer, string data)
        {
            try
            {
                using var p = new ProducerBuilder<string, string>(_config).Build();
                await p.ProduceAsync(service, new Message<string, string>
                {
                    Value = data,
                    Key = consumer
                });
            }
            catch (Exception e)
            {
                _logger.Fatal(e, $"Exception on send message with kafka to service {service}");
            }
        }

        public Broker GetBroker()
        {
            return Broker.Kafka;
        }
    }
}
