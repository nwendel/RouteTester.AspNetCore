using System.Net;
using Xunit;

namespace MvcRouteTester.AspNetCore.Builders;

public class RouteTesterNotFoundRouteAssert : IRouteAssert
{
    public Task AssertExpectedAsync(HttpResponseMessage responseMessage)
    {
        GuardAgainst.Null(responseMessage);

        if (responseMessage.StatusCode != HttpStatusCode.NotFound)
        {
            // TODO: Remove Xunit usage
            Assert.False(true, "Status code is not 404 (Not Found)");
        }

        return Task.CompletedTask;
    }
}
