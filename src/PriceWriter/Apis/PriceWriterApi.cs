using Microsoft.AspNetCore.Mvc;
using PriceWriter.Model;

namespace PriceWriter.Apis;

public static class PriceWriterApi
{
    public static IEndpointRouteBuilder MapPriceWriterApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/message", GetMessage).WithName("GetMessage");
        
        return app;
    }
    
    public static async Task<string> GetMessage(
        [FromServices] PriceWriterServices services)
    {
        services.Logger.LogInformation("Test!");
        
        return await Task.FromResult("Hello from PriceWriter.");
    }
}