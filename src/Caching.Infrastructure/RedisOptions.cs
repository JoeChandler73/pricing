namespace Caching.Infrastructure;

public record RedisOptions
{
    public string ConnectionString { get; init; }
}