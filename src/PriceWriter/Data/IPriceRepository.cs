namespace PriceWriter.Data;

public interface IPriceRepository
{
    Task<List<Price>> GetPrices();
    
    Task AddPrice(Price price);
}