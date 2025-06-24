namespace PriceReader.Apis;

public static class PriceReaderApi
{
    public static IEndpointRouteBuilder MapPriceReaderApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/message", GetMessage).WithName("GetMessage");
        
        return app;
    }
    
    public static async Task<string> GetMessage(
        [FromServices] PriceReaderServices services)
    {
        services.Logger.LogInformation("Test!");
        
        return await Task.FromResult("Hello from PriceReader.");
    }
}