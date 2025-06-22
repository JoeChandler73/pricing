namespace PriceManager.Model;

public class PriceManagerServices(
    ILogger<PriceManagerServices> _logger)
{
    public ILogger<PriceManagerServices> Logger => _logger;
}