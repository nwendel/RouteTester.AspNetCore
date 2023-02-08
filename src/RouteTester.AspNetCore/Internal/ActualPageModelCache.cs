namespace RouteTester.AspNetCore.Internal;

public class ActualPageModelCache
{
    private readonly Dictionary<string, object?> _cache = new();

    public object? this[string key]
    {
        get
        {
            lock (_cache)
            {
                return _cache[key];
            }
        }
    }

    public void Add(string key, object? actualActionInvokeInfo)
    {
        lock (_cache)
        {
            _cache.Add(key, actualActionInvokeInfo);
        }
    }

    public void Remove(string key)
    {
        lock (_cache)
        {
            _cache.Remove(key);
        }
    }
}
