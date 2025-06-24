using Caching.Application;
using Caching.Infrastructure;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace PriceReader.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
        builder.Services.AddSingleton<PriceReaderServices>();
        
        builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection("Redis"));
        builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<RedisOptions>>();
            var redis = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
            return redis;
        });
        builder.Services.AddSingleton<ICache, RedisCache>();
    }
}