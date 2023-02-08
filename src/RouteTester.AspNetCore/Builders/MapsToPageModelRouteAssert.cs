using RouteTester.AspNetCore.Internal;

namespace RouteTester.AspNetCore.Builders;

public class MapsToPageModelRouteAssert : IRouteAssert
{
    private readonly ActualPageModelCache _actualPageModelCache;

    private Type? _expectedPageModelType;

    public MapsToPageModelRouteAssert(ActualPageModelCache actualPageModelCache)
    {
        _actualPageModelCache = actualPageModelCache;
    }

    public void Expectation(Type type)
    {
        GuardAgainst.Null(type);

        _expectedPageModelType = type;
    }

    public async Task AssertExpectedAsync(HttpResponseMessage responseMessage)
    {
        GuardAgainst.Null(responseMessage);

        responseMessage.EnsureSuccessStatusCode();

        var key = await responseMessage.Content.ReadAsStringAsync();
        var actualPageModel = _actualPageModelCache[key];
        _actualPageModelCache.Remove(key);

        TestFramework.Equal(_expectedPageModelType, actualPageModel?.GetType());
    }
}
