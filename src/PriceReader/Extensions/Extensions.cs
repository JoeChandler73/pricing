using PriceReader.Model;

namespace PriceReader.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<PriceReaderServices>();
    }
}