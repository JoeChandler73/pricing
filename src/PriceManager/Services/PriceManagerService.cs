using Messaging.Application;
using PriceManager.Messages;
using PriceManager.Model;

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
            foreach (var symbol in _subscriptionManager.GetSymbols())
            {
                var price = new Price(symbol, 100, DateTime.Now);
                await _messageBus.SendAsync(price);
            }
        }
    }
}