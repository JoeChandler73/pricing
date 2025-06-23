using Messaging.Application;

namespace PriceWriter.Services;

public class PriceWriterService(IMessageBus _messageBus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageBus.InitialiseAsync();
    }
}