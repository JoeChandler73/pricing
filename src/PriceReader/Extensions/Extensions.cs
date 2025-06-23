using Messaging.Infrastructure;
using PriceReader.Model;

namespace PriceReader.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
        builder.Services.AddSingleton<PriceReaderServices>();
    }
}