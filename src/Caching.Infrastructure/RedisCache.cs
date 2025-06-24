using System.Text.Json;
using Caching.Application;
using StackExchange.Redis;

namespace Caching.Infrastructure;

public class RedisCache(IConnectionMultiplexer _redis) : ICache
{
    public async Task SetValueAsync<T>(string key, T value, TimeSpan expiration)
    {
        var db = _redis.GetDatabase();
        var json = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, json, expiration);
    }

    public async Task<T> GetValueAsync<T>(string key)
    {
        var db = _redis.GetDatabase();
        var json = await db.StringGetAsync(key);
        return json.HasValue ?  JsonSerializer.Deserialize<T>(json) : default;
    }
}