public interface ICachingService<T>
{
    Task<T?> GetAsync(string key);
    Task SetAsync(string key, T value);
}