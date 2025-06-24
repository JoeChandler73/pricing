namespace PriceWriter.Services;

public class PriceWriterService(
    IMessageBus _messageBus,
    ILogger<PriceWriterService> _logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageBus.Subscribe<Price>(HandlePrice);
        await _messageBus.InitialiseAsync();
    }

    private async Task HandlePrice(Price message)
    {
        _logger.LogInformation("PriceWriterService received: {price}", message);
        
        await Task.CompletedTask;
    }
}