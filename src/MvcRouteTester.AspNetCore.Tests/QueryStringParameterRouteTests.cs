#region License
// Copyright (c) Niklas Wendel 2018-2019
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
    public class QueryStringParameterRouteTests : IClassFixture<TestServerFixture>
    {

        /// <summary>
        /// 
        /// </summary>
        private readonly TestServer _server;

        /// <summary>
        /// 
        /// </summary>
        public QueryStringParameterRouteTests(TestServerFixture testServerFixture)
        {
            _server = testServerFixture.Server;
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task CanRouteWithParameter()
        {
            await RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
                routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter("value")));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task ThrowsOnRouteWithParameterWrongValue()
        {
            await Assert.ThrowsAsync<EqualException>(() =>
               RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
                    routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter("wrong-value"))));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task CanRouteWithoutParameter()
        {
            await RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter"),
                routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter(null)));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task CanRouteWithParameterMatchingAny()
        {
            await RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
                routeAssert => routeAssert.MapsTo<ParameterController>(a => a.QueryStringParameter(Args.Any<string>())));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task CanRouteWithParameterForParameterExpression()
        {
            await RouteAssert.ForAsync(
                _server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
                routeAssert => routeAssert
                    .MapsTo<ParameterController>(a => a.QueryStringParameter(Args.Any<string>()))
                    .ForParameter<string>("parameter", p =>
                    {
                        Assert.True(p == "value");
                    }));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task ThrowsOnRouteWithParameterForParameterExpressionWrongValue()
        {
            await Assert.ThrowsAsync<EqualException>(() =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=wrong-value"),
                    routeAssert => routeAssert
                        .MapsTo<ParameterController>(a => a.QueryStringParameter(Args.Any<string>()))
                        .ForParameter<string>("parameter", p =>
                        {
                            Assert.Equal("value", p);
                        })));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task ThrowsOnRouteWithParameterForParameterWrongParameterName()
        {
            await Assert.ThrowsAsync<ArgumentException>("parameterName", () =>
                RouteAssert.ForAsync(
                    _server,
                    request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=wrong-value"),
                    routeAssert => routeAssert
                        .MapsTo<ParameterController>(a => a.QueryStringParameter(Args.Any<string>()))
                        .ForParameter<string>("wrong-parameter", p =>
                        {
                            Assert.Equal("value", p);
                        })));
        }
        
    }

}
