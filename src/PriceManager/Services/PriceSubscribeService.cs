using Messaging.Application;
using PriceManager.Messages;

namespace PriceManager.Services;

public class PriceSubscribeService(
    IMessageBus _messageBus,
    ILogger<PriceSubscribeService> _logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageBus.Subscribe<PriceSubscribe>(HandlePriceSubscribe);
        await _messageBus.InitialiseAsync();
    }

    private async Task HandlePriceSubscribe(PriceSubscribe message)
    {
        _logger.LogInformation("Handling PriceSubscribe for symbol: {Symbol}", message.Symbol);
        
        await Task.CompletedTask;
    }
}