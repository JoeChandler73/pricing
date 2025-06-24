namespace PriceManager.Apis;

public static class PriceManagerApi
{
    public static IEndpointRouteBuilder MapPriceManagerApi(this IEndpointRouteBuilder app)
    {
        app.MapPut("subscribe", Subscribe).WithName("Subscribe");
        
        return app;
    }

    private static async Task Subscribe(
        [FromServices] PriceManagerServices services,
        PriceSubscribe message)
    {
        services.Logger.LogInformation("Received PriceSubscribe message: {Message}", message); 
        services.Logger.LogInformation("Publishing PriceSubscribe message to the message bus");
        
        await services.MessageBus.SendAsync(message);
    }
}