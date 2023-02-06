using RouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;
using TestApplication.Model;

namespace RouteTester.AspNetCore.Tests;

public sealed class PostFormDataTests : IDisposable
{
    private readonly TestApplicationFactory _factory = new();

    [Fact]
    public async Task CanPostPerson()
    {
        await RouteAssert.ForAsync(
            _factory.Server,
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
                    Assert.Equal("Niklas", p?.FirstName);
                    Assert.Equal("Wendel", p?.LastName);
                }));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
