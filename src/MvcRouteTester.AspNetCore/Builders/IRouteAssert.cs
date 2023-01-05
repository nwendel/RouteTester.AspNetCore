namespace MvcRouteTester.AspNetCore.Builders;

public interface IRouteAssert
{
    Task AssertExpectedAsync(HttpResponseMessage responseMessage);
}
