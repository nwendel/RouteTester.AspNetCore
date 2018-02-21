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
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using TestWebApplication.Controllers;
using TestWebApplication.Model;

namespace MvcRouteTester.AspNetCore.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class PostFormDataTests : IClassFixture<TestServerFixture>
    {

        /// <summary>
        /// 
        /// </summary>
        private readonly TestServer _server;

        /// <summary>
        /// 
        /// </summary>
        public PostFormDataTests(TestServerFixture testServerFixture)
        {
            _server = testServerFixture.Server;
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanPostPerson()
        {
            RouteAssert.For(
                _server,
                request => request
                    .WithPathAndQuery("/post-with-person")
                    .WithMethod(HttpMethod.Post)
                    .WithFormData(new Dictionary<string, string>
                    {
                        { "FirstName", "Niklas" },
                        { "LastName", "Wendel" }
                    }),
                routeAssert => routeAssert
                    .MapsTo<PostController>(a => a.WithPerson(Args.Any<Person>()))
                    .ForParameter<Person>("person", p =>
                    {
                        Assert.Equal("Niklas", p.FirstName);
                        Assert.Equal("Wendel", p.LastName);
                    }));
        }

    }

}
