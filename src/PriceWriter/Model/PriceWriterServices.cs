namespace PriceWriter.Model;

public class PriceWriterServices(
    ILogger<PriceWriterServices> _logger)
{
    public ILogger<PriceWriterServices> Logger => _logger;
}