namespace RouteTester.AspNetCore.Internal;

public interface IRouteAssert
{
    Task AssertExpectedAsync(HttpResponseMessage responseMessage);
}
