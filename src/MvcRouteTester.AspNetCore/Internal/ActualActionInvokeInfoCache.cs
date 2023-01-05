namespace MvcRouteTester.AspNetCore.Internal;

public class ActualActionInvokeInfoCache
{
    private readonly Dictionary<string, ActualActionInvokeInfo> _cache = new();

    public ActualActionInvokeInfo this[string key]
    {
        get
        {
            lock (_cache)
            {
                return _cache[key];
            }
        }
    }

    public void Add(string key, ActualActionInvokeInfo actualActionInvokeInfo)
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
