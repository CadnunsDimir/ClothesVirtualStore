using System.Text.Json;
using ClothesVirtualStore.Api.Cart.Models;
using Microsoft.Extensions.Caching.Distributed;

public class CachingService : ICachingService<Cart>
{
    private readonly IDistributedCache cache;
    private readonly DistributedCacheEntryOptions options;

    public CachingService(IDistributedCache cache)
    {
        this.cache = cache;
        this.options = new DistributedCacheEntryOptions{
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };
    }
    public async Task<Cart?> GetAsync(string key)
    {
        var valueCache = await cache.GetStringAsync(key);
        return !string.IsNullOrEmpty(valueCache)? JsonSerializer.Deserialize<Cart?>(valueCache) : null;
    }

    public async Task SetAsync(string key, Cart value)
    {
        var valueCache = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, valueCache, options);
    }
}