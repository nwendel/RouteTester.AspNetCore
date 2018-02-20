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
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using TestWebApplication.Controllers;

namespace MvcRouteTester.AspNetCore.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class BasicRouteTests : IClassFixture<TestServerFixture>
    {

        /// <summary>
        /// 
        /// </summary>
        private readonly TestServer _server;

        /// <summary>
        /// 
        /// </summary>
        public BasicRouteTests(TestServerFixture testServerFixture)
        {
            _server = testServerFixture.Server;
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanGetSimpleAttributeRoute()
        {
            RouteAssert.For(
                _server,
                request => request.WithPathAndQuery("/simple-attribute-route"),
                routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRoute()));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanGetSimpleAttributeRouteAsync()
        {
            RouteAssert.For(
                _server,
                request => request.WithPathAndQuery("/simple-attribute-route-async"),
                routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRouteAsync()));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanPostSimpleAttributeRoute()
        {
            RouteAssert.For(
                _server,
                request => request
                    .WithMethod(HttpMethod.Post)
                    .WithPathAndQuery("/simple-attribute-route-post"),
                routeAssert => routeAssert.MapsTo<PostController>(a => a.SimpleAttributeRoute()));
        }

    }

}
