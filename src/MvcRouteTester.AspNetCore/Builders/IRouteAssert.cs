using System.Net.Http;
using System.Threading.Tasks;

namespace MvcRouteTester.AspNetCore.Builders
{
    public interface IRouteAssert
    {
        Task AssertExpectedAsync(HttpResponseMessage responseMessage);
    }
}
