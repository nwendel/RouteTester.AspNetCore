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
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Xunit.Sdk;
using TestWebApplication.Controllers;

namespace MvcRouteTester.AspNetCore.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class BasicIncorrectRouteTests : IClassFixture<TestServerFixture>
    {

        /// <summary>
        /// 
        /// </summary>
        private readonly TestServer _server;

        /// <summary>
        /// 
        /// </summary>
        public BasicIncorrectRouteTests(TestServerFixture testServerFixture)
        {
            _server = testServerFixture.Server;
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task ThrowsOnMapsToIncorrectController()
        {
            await Assert.ThrowsAsync<EqualException>(() =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/simple-attribute-route"),
                    routeAssert => routeAssert.MapsTo<InvalidController>(a => a.Default())));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task ThrowsOnMapsToIncorrectActionMethodName()
        {
            await Assert.ThrowsAsync<EqualException>(() =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/simple-attribute-route"),
                    routeAssert => routeAssert.MapsTo<HomeController>(a => a.SimpleAttributeRouteAsync())));
        }

    }

}
