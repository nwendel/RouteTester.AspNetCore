using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using MvcRouteTester.AspNetCore.Infrastructure;
using TestWebApplication.Controllers;
using TestWebApplication.Model;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests
{
    public class PostJsonDataTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServer _server;

        public PostJsonDataTests(TestServerFixture testServerFixture)
        {
            GuardAgainst.Null(testServerFixture);

            _server = testServerFixture.Server;
        }

        [Fact]
        public async Task CanPostJsonPerson()
        {
            await RouteAssert.ForAsync(
                _server,
                request => request
                    .WithPathAndQuery("/post-with-json-person")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonData(new Person
                    {
                        FirstName = "Niklas",
                        LastName = "Wendel",
                    }),
                routeAssert => routeAssert
                    .MapsTo<PostController>(a => a.WithJsonPerson(Args.Any<Person>()))
                    .ForParameter<Person>("person", p =>
                    {
                        Assert.Equal("Niklas", p!.FirstName);
                        Assert.Equal("Wendel", p!.LastName);
                    }));
        }
    }
}
