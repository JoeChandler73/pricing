namespace Caching.Application;

public interface ICache
{
    Task SetValueAsync<T>(string key, T value, TimeSpan expiration);
    
    Task<T> GetValueAsync<T>(string key);
}