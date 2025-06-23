using System.Collections.Concurrent;

namespace PriceManager.Model;

public class SubscriptionManager : ISubscriptionManager
{
    private readonly ConcurrentDictionary<string, byte> _symbols = new();
    
    public void Add(string symbol)
    {
        _symbols.TryAdd(symbol, 0);
    }

    public IEnumerable<string> GetSymbols()
    {
        return _symbols.Keys;
    }
}