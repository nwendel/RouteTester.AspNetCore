using MvcRouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;
using TestApplication.Model;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests;

public sealed class PostJsonDataTests : IDisposable
{
    private readonly TestApplicationFactory _factory;

    public PostJsonDataTests()
    {
        _factory = new TestApplicationFactory();
    }

    [Fact]
    public async Task CanPostJsonPerson()
    {
        await RouteAssert.ForAsync(
            _factory.Server,
            request => request
                .WithPathAndQuery("/post-with-json-person")
                .WithMethod(HttpMethod.Post)
                .WithJsonContent(new Person
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

    public void Dispose()
    {
        _factory.Dispose();
    }
}
