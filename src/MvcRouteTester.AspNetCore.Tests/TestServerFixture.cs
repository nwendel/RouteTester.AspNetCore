﻿#region License
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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TestWebApplication;

namespace MvcRouteTester.AspNetCore.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class TestServerFixture
    {

        /// <summary>
        /// 
        /// </summary>
        public TestServerFixture()
        {
            Server = new TestServer(new WebHostBuilder().UseTestStartup<TestStartup, Startup>());
        }

        /// <summary>
        /// 
        /// </summary>
        public TestServer Server { get; }

        /// <summary>
        /// 
        /// </summary>
        public class TestStartup
        {

            /// <summary>
            /// 
            /// </summary>
            /// <param name="serviceCollection"></param>
            public void ConfigureServices(IServiceCollection serviceCollection)
            {
                serviceCollection.AddMvc();
                serviceCollection.AddMvcRouteTester();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="app"></param>
            public void Configure(IApplicationBuilder app)
            {
                app.UseRouting();

                app.UseEndpoints(x =>
                {
                    x.MapControllers();
                });
            }

        }

    }

}
