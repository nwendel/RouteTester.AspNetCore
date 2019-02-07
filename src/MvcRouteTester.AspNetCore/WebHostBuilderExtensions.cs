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
using Microsoft.AspNetCore.Hosting;

namespace MvcRouteTester.AspNetCore
{

    /// <summary>
    /// 
    /// </summary>
    public static class WebHostBuilderExtensions
    {

        #region Use Test Startup

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTestStartup"></typeparam>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseTestStartup<TTestStartup, TStartup>(this IWebHostBuilder self)
            where TTestStartup : class
            where TStartup : class
        {
            var applicationKey = typeof(TStartup).Assembly.FullName;

            return self
                .UseStartup<TTestStartup>()
                .UseSetting(WebHostDefaults.ApplicationKey, applicationKey);
        }

        #endregion

    }

}
