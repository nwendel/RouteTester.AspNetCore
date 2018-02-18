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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using MvcRouteTester.AspNetCore.Internal;

namespace MvcRouteTester.AspNetCore
{

    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        #region Add Route Testing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddRouteTesting(this IServiceCollection serviceCollection)
        {
            serviceCollection.RemoveWhere(x => x.ImplementationType == typeof(AuthorizationApplicationModelProvider));

            serviceCollection.RemoveWhere(x => x.ServiceType == typeof(IActionInvokerFactory));
            serviceCollection.AddSingleton<IActionInvokerFactory, RouteTestingActionInvokerFactory>();
        }

        #endregion

    }

}
