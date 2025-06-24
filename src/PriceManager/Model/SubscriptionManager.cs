namespace PriceManager.Model;

public class SubscriptionManager : ISubscriptionManager
{
    private readonly ConcurrentDictionary<string, Price> _prices = new();
    
    public void Add(string symbol)
    {
        _prices.TryAdd(symbol, new Price(symbol, 100, DateTime.UtcNow));
    }

    public IEnumerable<Price> GetLastPrices()
    {
        return _prices.Values;
    }
}