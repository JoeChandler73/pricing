namespace PriceManager.Services;

public class PriceSubscribeService(
    IMessageBus _messageBus,
    ISubscriptionManager _subscriptionManager,
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
        
        _subscriptionManager.Add(message.Symbol);
        
        await Task.CompletedTask;
    }
}