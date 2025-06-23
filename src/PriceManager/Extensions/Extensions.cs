using Messaging.Application;
using Messaging.Infrastructure;
using PriceManager.Model;
using PriceManager.Services;

namespace PriceManager.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
        builder.Services.AddSingleton<IMessageBus, RabbitMqMessageBus>();
        builder.Services.AddSingleton<PriceManagerServices>();
        builder.Services.AddHostedService<PriceSubscribeService>();
    }
}