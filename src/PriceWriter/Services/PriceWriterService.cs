using AutoMapper;
using Caching.Application;
using PriceWriter.Data;

namespace PriceWriter.Services;

public class PriceWriterService(
    IMessageBus _messageBus,
    IPriceRepository _priceRepository,
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

        var tasks = new[]
        {
            _cache.SetValueAsync(message.Symbol, message, CacheExpiration),
            _priceRepository.AddPrice(message)
        };
        
        await Task.WhenAll(tasks);
    }
}