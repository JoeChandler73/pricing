using AutoMapper;
using Caching.Application;
using Caching.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PriceWriter.Data;
using StackExchange.Redis;

namespace PriceWriter.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContextFactory<PricingDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));
        
        builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
        builder.Services.AddSingleton<IMessageSerializer, JsonMessageSerialzer>();
        builder.Services.AddSingleton<IMessageBus, RabbitMqMessageBus>();
        builder.Services.AddSingleton<PriceWriterServices>();
        builder.Services.AddSingleton<IPriceRepository, PriceRepository>();
        builder.Services.AddHostedService<PriceWriterService>();

        builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection("Redis"));
        builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<RedisOptions>>();
            var redis = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
            return redis;
        });
        builder.Services.AddSingleton<ICache, RedisCache>();
        
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new PriceWriterMappingProfile());
        });

        var mapper = mappingConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);
    }
}