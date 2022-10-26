using Confluent.Kafka;
using Confluent.Kafka.Admin;
using FluxoCaixa.Chassis.Logging.Interfaces;
using FluxoCaixa.Chassis.Messaging.Interfaces;
using FluxoCaixa.Chassis.Utils.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FluxoCaixa.Chassis.Messaging.Kafka
{
    internal class KafkaConsumer : BackgroundService
    {
        private readonly IErrorLogger _logger;
        private readonly IEnumerable<IConsumer> _consumers;
        private readonly IConsumer<string, string> _consumer;
        private readonly ConsumerConfig _config;
        private readonly string _service;

        public KafkaConsumer(IConfiguration configuration, IErrorLogger logger, IAssemblyHelper assemblyHelper, IEnumerable<IConsumer> consumers)
        {
            _consumers = consumers;
            _logger = logger;
            _service = assemblyHelper.GetServiceName();
            _config = new ConsumerConfig
            {
                GroupId = "FluxoCaixa",
                BootstrapServers = configuration.GetValue<string>("KafkaAddress"),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                AllowAutoCreateTopics = true
            };
            _consumer = new ConsumerBuilder<string, string>(_config).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await CreateTopicIfNotExistsAsync();
            _consumer.Subscribe(_service);

            await Task.Factory.StartNew(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consume = _consumer.Consume();
                        var consumer = _consumers.FirstOrDefault(f => f.GetKey().Equals(consume.Message.Key));
                        if (consumer == null)
                            throw new ArgumentException($"Consumer with name {consume.Message.Key} not found on service {_service}");

                        await Task.Run(() =>
                        {
                            consumer.OnExecuting(consume.Message.Value);
                        }, cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        _logger.Fatal(e, $"Exception on receive message on service {_service}");
                    }
                }

            }, cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            await Task.CompletedTask;
        }

        public override void Dispose()
        {
            _consumer?.Dispose();
        }

        private async Task CreateTopicIfNotExistsAsync()
        {
            try
            {
                using var adminClient = new AdminClientBuilder(_config).Build();
                var topics = adminClient.GetMetadata(_service, TimeSpan.FromSeconds(5))?.Topics?.Select(s => s.Topic).ToList();
                if (topics != null && topics.Any() && topics.Contains(_service))
                    return;

                await adminClient.CreateTopicsAsync(new TopicSpecification[]
                {
                    new()
                    {
                        Name = _service,
                        ReplicationFactor = 1,
                        NumPartitions = 1
                    }
                });
            }
            catch (Exception e)
            {
                _logger.Fatal(e, $"Exception on create a new topic {_service} on kafka");
            }
        }
    }
}
