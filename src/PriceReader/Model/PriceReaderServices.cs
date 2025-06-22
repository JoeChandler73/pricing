namespace PriceReader.Model;

public class PriceReaderServices(
    ILogger<PriceReaderServices> _logger)
{
    public ILogger<PriceReaderServices> Logger => _logger;
}