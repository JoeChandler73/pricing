using Caching.Application;

namespace PriceReader.Model;

public class PriceReaderServices(
    ICache _cache,
    ILogger<PriceReaderServices> _logger)
{
    public ICache Cache => _cache;
    
    public ILogger<PriceReaderServices> Logger => _logger;
}