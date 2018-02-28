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
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MvcRouteTester.AspNetCore.Builders;

namespace MvcRouteTester.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class RouteAssert
    {

        #region For

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="requestBuilder"></param>
        /// <param name="routeAssertBuilder"></param>
        public static void For(TestServer server, Action<IRequestBuilder> requestBuilder, Action<IRouteAssertBuilder> routeAssertBuilder)
        {
            Task.Run(async () => await ForAsync(server, requestBuilder, routeAssertBuilder))
                .GetAwaiter().GetResult();
        }

        #endregion

        #region For Async

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="requestBuilder"></param>
        /// <param name="routeAssertBuilder"></param>
        public static async Task ForAsync(TestServer server, Action<IRequestBuilder> requestBuilder, Action<IRouteAssertBuilder> routeAssertBuilder)
        {
            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }
            if (requestBuilder == null)
            {
                throw new ArgumentNullException(nameof(requestBuilder));
            }
            if (routeAssertBuilder == null)
            {
                throw new ArgumentNullException(nameof(routeAssertBuilder));
            }

            var serviceProvider = server.Host.Services;
            var request = serviceProvider.GetRequiredService<RouteTesterRequest>();
            var routeAssert = serviceProvider.GetRequiredService<RouteTesterRouteAssert>();

            requestBuilder(request);
            routeAssertBuilder(routeAssert);

            var responseMessage = await request.ExecuteAsync(server);
            await routeAssert.AssertExpectedAsync(responseMessage);
        }

        #endregion
        
    }

}
