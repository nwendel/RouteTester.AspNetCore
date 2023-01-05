using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MvcRouteTester.AspNetCore.Builders
{
    public class RouteTesterNotFoundRouteAssert : IRouteAssert
    {
        public Task AssertExpectedAsync(HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode != HttpStatusCode.NotFound)
            {
                // TODO: Remove Xunit usage
                Assert.False(true, "Status code is not 404 (Not Found)");
            }

            return Task.CompletedTask;
        }
    }
}
