namespace PriceManager.Model;

public interface ISubscriptionManager
{
    void Add(string symbol);
    
    IEnumerable<string> GetSymbols();
}