using PriceWriter.Model;

namespace PriceWriter.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<PriceWriterServices>();
    }
}