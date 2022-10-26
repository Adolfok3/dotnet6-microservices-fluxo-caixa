using FluxoCaixa.Chassis.Messaging.Configuration;
using FluxoCaixa.Chassis.Messaging.Interfaces;
using FluxoCaixa.Chassis.Messaging.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Chassis.Messaging.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddMessaging(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IProducerStrategy, ProducerStrategy>();
            builder.Services.AddTransient<IProducer, Producer>();

            // Kafka Integration
            builder.Services.AddTransient<IProducerClient, KafkaProducer>();
            builder.Services.AddHostedService<KafkaConsumer>();
        }
    }
}
