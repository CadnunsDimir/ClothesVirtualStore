using ClothesVirtualStore.Api.Cart.Models;
using Microsoft.Extensions.Caching.Distributed;

public class CachingService : ICachingService
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
    public async Task<string?> GetAsync(string key)
    {
        return await cache.GetStringAsync(key);
    }

    public async Task SetAsync(string key, string value)
    {
        await cache.SetStringAsync(key, value, options);
    }
}