using Microsoft.AspNetCore.TestHost;
using MvcRouteTester.AspNetCore.Infrastructure;
using TestWebApplication.Controllers;
using TestWebApplication.Model;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests;

public class PostFormDataTests : IClassFixture<TestServerFixture>
{
    private readonly TestServer _server;

    public PostFormDataTests(TestServerFixture testServerFixture)
    {
        GuardAgainst.Null(testServerFixture);

        _server = testServerFixture.Server;
    }

    [Fact]
    public async Task CanPostPerson()
    {
        await RouteAssert.ForAsync(
            _server,
            request => request
                .WithPathAndQuery("/post-with-person")
                .WithMethod(HttpMethod.Post)
                .WithFormContent(new Dictionary<string, string>
                {
                    { "FirstName", "Niklas" },
                    { "LastName", "Wendel" },
                }),
            routeAssert => routeAssert
                .MapsTo<PostController>(a => a.WithPerson(Args.Any<Person>()))
                .ForParameter<Person>("person", p =>
                {
                    Assert.Equal("Niklas", p!.FirstName);
                    Assert.Equal("Wendel", p!.LastName);
                }));
    }
}
