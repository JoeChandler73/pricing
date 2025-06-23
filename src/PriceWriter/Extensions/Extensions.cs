using Messaging.Infrastructure;
using PriceWriter.Model;

namespace PriceWriter.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
        builder.Services.AddSingleton<PriceWriterServices>();
    }
}