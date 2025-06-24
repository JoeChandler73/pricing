namespace PriceManager.Services;

public class PriceManagerService(
    IMessageBus _messageBus,
    ISubscriptionManager _subscriptionManager) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            foreach (var lastPrice in _subscriptionManager.GetLastPrices())
            {
                await _messageBus.SendAsync(GetNewPrice(lastPrice));
            }
        }
    }

    private static Price GetNewPrice(Price price)
    {
        return new Price(price.Symbol, GetNewPrice(price.Mid), DateTime.UtcNow);
    }

    private static decimal GetNewPrice(decimal price)
    {
        var adjustment = (decimal)Random.Shared.NextDouble() - 0.5m;
        return price + adjustment;
    }
}