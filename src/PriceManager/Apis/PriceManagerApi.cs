using Microsoft.AspNetCore.Mvc;
using PriceManager.Model;

namespace PriceManager.Apis;

public static class PriceManagerApi
{
    public static IEndpointRouteBuilder MapPriceManagerApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/message", GetMessage).WithName("GetMessage");
        
        return app;
    }

    public static async Task<string> GetMessage(
       [FromServices] PriceManagerServices services)
    {
        services.Logger.LogInformation("Test!");
        
        return await Task.FromResult("Hello from PriceManager.");
    }
}