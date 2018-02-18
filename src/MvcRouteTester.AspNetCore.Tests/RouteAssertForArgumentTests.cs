#region License
// Copyright (c) Niklas Wendel 2018
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
#endregion

using System;
using Microsoft.AspNetCore.TestHost;
using TestWebApplication.Controllers;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class RouteAssertForArgumentTests : IClassFixture<TestServerFixture>
    {


        /// <summary>
        /// 
        /// </summary>
        private readonly TestServer _server;

        /// <summary>
        /// 
        /// </summary>
        public RouteAssertForArgumentTests(TestServerFixture testServerFixture)
        {
            _server = testServerFixture.Server;
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnNullServer()
        {
            Assert.Throws<ArgumentNullException>("server", () =>
                RouteAssert.For(
                    null,
                    request => request.WithPathAndQuery("/simple-attribute-route"),
                    route => route.MapsTo<HomeController>(a => a.SimpleAttributeRoute())));

        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnNullRequestBuilder()
        {
            Assert.Throws<ArgumentNullException>("requestBuilder", () =>
                RouteAssert.For(
                    _server,
                    null,
                    route => route.MapsTo<HomeController>(a => a.SimpleAttributeRoute())));

        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnNullRouteAssertBuilder()
        {
            Assert.Throws<ArgumentNullException>("routeAssertBuilder", () =>
                RouteAssert.For(
                    _server,
                    request => request.WithPathAndQuery("/simple-attribute-route"),
                    null));

        }

    }

}
