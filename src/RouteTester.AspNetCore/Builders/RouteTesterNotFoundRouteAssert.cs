using System.Net;

namespace RouteTester.AspNetCore.Builders;

public class RouteTesterNotFoundRouteAssert : IRouteAssert
{
    public Task AssertExpectedAsync(HttpResponseMessage responseMessage)
    {
        GuardAgainst.Null(responseMessage);

        TestFramework.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        return Task.CompletedTask;
    }
}
