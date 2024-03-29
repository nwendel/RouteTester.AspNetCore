﻿using RouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;
using TestApplication.Model;

namespace RouteTester.AspNetCore.Tests;

public sealed class PostJsonDataTests : IDisposable
{
    private readonly TestApplicationFactory _factory = new();

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
            assert => assert
                .MapsToControllerAction<PostController>(a => a.WithJsonPerson(Args.Any<Person>()))
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
