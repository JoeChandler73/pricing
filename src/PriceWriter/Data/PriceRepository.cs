using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace PriceWriter.Data;

public class PriceRepository(
    IDbContextFactory<PricingDbContext> _factory,
    IMapper _mapper) 
    : IPriceRepository
{
    public async Task<List<Price>> GetPrices()
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        
        var prices = await dbContext.Prices.ToListAsync();

        return prices
            .Select(_mapper.Map<Price>)
            .ToList();
    }

    public async Task AddPrice(Price price)
    {
        var priceData = _mapper.Map<PriceData>(price);
        
        await using var dbContext = await _factory.CreateDbContextAsync();
        
        dbContext.Prices.Add(priceData);
        await dbContext.SaveChangesAsync();
    }
}