using Messaging.Application;

namespace PriceManager.Model;

public class PriceManagerServices(
    IMessageBus _messageBus,
    ILogger<PriceManagerServices> _logger)
{
    public IMessageBus MessageBus => _messageBus;
    
    public ILogger<PriceManagerServices> Logger => _logger;
}