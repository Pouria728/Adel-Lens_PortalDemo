using HamrahanSystem.Application.UseCaseInterface;
using Microsoft.Extensions.Caching.Memory;

namespace HamrahanSystem.Application.UseCaseImplementation;

public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _defaultCacheDuration;

    public InMemoryCacheService(IMemoryCache cache, TimeSpan defaultCacheDuration)
    {
        _cache = cache;
        _defaultCacheDuration = defaultCacheDuration <= TimeSpan.Zero
            ? TimeSpan.FromMinutes(60)
            : defaultCacheDuration;
    }

    public T GetData<T>(string key)
    {
        return _cache.TryGetValue(key, out T? value) ? value! : default!;
    }

    public bool SetData<T>(string key, T value)
    {
        _cache.Set(key, value, _defaultCacheDuration);
        return true;
    }

    public object RemoveData(string key)
    {
        _cache.Remove(key);
        return true;
    }
}
