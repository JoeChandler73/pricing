using PriceReader.Messages;

namespace PriceReader.Apis;

public static class PriceReaderApi
{
    public static IEndpointRouteBuilder MapPriceReaderApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/price", GetPrice).WithName("GetPrice");
        
        return app;
    }

    public static async Task<decimal> GetPrice(
        [FromServices] PriceReaderServices services,
        string symbol)
    {
        services.Logger.LogInformation("Received GetPrice request for symbol: {Symbol}", symbol);
        
        var price = await services.Cache.GetValueAsync<Price>(symbol);
        
        return price?.Mid ?? 0;
    }
}