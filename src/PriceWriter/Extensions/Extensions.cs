using Messaging.Application;
using Messaging.Infrastructure;
using PriceWriter.Model;
using PriceWriter.Services;

namespace PriceWriter.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
        builder.Services.AddSingleton<IMessageBus, RabbitMqMessageBus>();
        builder.Services.AddSingleton<PriceWriterServices>();
        builder.Services.AddHostedService<PriceWriterService>();
    }
}