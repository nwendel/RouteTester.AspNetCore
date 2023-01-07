﻿using MvcRouteTester.AspNetCore.Tests.TestHelpers;
using TestApplication.Controllers;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests;

public sealed class BasicRouteTests : IDisposable
{
    private readonly TestApplicationFactory _factory;

    public BasicRouteTests()
    {
        _factory = new TestApplicationFactory();
    }

    [Fact]
    public async Task CanGetSimpleAttributeRoute()
    {
        await RouteAssert.ForAsync(
            _factory,
            request => request.WithPathAndQuery("/simple-attribute-route"),
            routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRoute()));
    }

    [Fact]
    public async Task CanGetSimpleAttributeRouteAsync()
    {
        await RouteAssert.ForAsync(
            _factory,
            request => request.WithPathAndQuery("/simple-attribute-route-async"),
            routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRouteAsync()));
    }

    [Fact]
    public async Task CanPostSimpleAttributeRoute()
    {
        await RouteAssert.ForAsync(
            _factory,
            request => request
                .WithMethod(HttpMethod.Post)
                .WithPathAndQuery("/simple-attribute-route-post"),
            routeAssert => routeAssert.MapsTo<PostController>(a => a.SimpleAttributeRoute()));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
