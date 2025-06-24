using Caching.Application;

namespace PriceWriter.Services;

public class PriceWriterService(
    IMessageBus _messageBus,
    ICache _cache,
    ILogger<PriceWriterService> _logger) : BackgroundService
{
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(10);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageBus.Subscribe<Price>(HandlePrice);
        await _messageBus.InitialiseAsync();
    }

    private async Task HandlePrice(Price message)
    {
        _logger.LogInformation("PriceWriterService received: {price}", message);
        _logger.LogInformation("PriceWriterService caching price");
        
        await _cache.SetValueAsync(message.Symbol, message, CacheExpiration);
    }
}