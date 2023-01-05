﻿using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void ThrowsOnAddRouteTeserBeforeAddMvc()
        {
            var tested = new ServiceCollection();

            Assert.Throws<MvcRouteTesterException>(() => tested.AddMvcRouteTester());
        }
    }
}