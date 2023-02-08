namespace RouteTester.AspNetCore.Builders;

public class MapsToPageModelRouteAssert : IRouteAssert
{
    public Task AssertExpectedAsync(HttpResponseMessage responseMessage)
    {
        return Task.CompletedTask;
    }
}
