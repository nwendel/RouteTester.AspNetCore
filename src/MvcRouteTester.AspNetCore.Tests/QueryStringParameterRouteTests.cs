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
using Microsoft.AspNetCore.TestHost;
using Xunit;
using TestWebApplication.Controllers;
using Xunit.Sdk;

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
        public void CanRouteWithParameter()
        {
            RouteAssert.For(
                _server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
                route => route.MapsTo<ParameterController>(a => a.QueryStringParameter("value")));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnRouteWithParameterWrongValue()
        {
            Assert.Throws<EqualException>(() =>
               RouteAssert.For(
                    _server,
                    request => request.WithPathAndQuery("/parameter/query-string-parameter?parameter=value"),
                    route => route.MapsTo<ParameterController>(a => a.QueryStringParameter("wrong-value"))));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRouteWithoutParameter()
        {
            RouteAssert.For(
                _server,
                request => request.WithPathAndQuery("/parameter/query-string-parameter"),
                route => route.MapsTo<ParameterController>(a => a.QueryStringParameter(null)));
        }

    }

}
