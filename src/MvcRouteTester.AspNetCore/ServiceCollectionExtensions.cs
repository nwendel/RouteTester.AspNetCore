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
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MvcRouteTester.AspNetCore.Builders;
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
        public static void AddMvcRouteTester(this IServiceCollection serviceCollection)
        {
            if (serviceCollection.All(x => x.ServiceType != typeof(IActionInvokerFactory)))
            {
                throw new MvcRouteTesterException("AddMvcRouteTester() must be called after AddMvc()");
            }

            // Register services needed by MvcRouteTester
            serviceCollection.AddTransient<RouteTesterRequest>();
            serviceCollection.AddTransient<RouteTesterRouteAssert>();
            serviceCollection.AddTransient<RouteTesterMapsToRouteAssert>();
            serviceCollection.AddTransient<RouteTesterNotFoundRouteAssert>();
            serviceCollection.AddSingleton<ActualActionInvokeInfoCache>();
            serviceCollection.AddSingleton<RouteExpressionParser>();

            serviceCollection.Configure<MvcOptions>(o => o.Filters.Add<MvcRouteTesterActionFilterAttribute>());

            /* serviceCollection.AddSingleton<ActionInvokerFactoryWrapper>(); */
            /*
            // Register Standard ActionInvoker as self instead of via interface since MvcRouteTester depends on it for binding arguments
            serviceCollection.AddSingleton<ActionInvokerFactory>();
            */

            /*
            // Replace standard IActionInvokerFactory implementation which one which records action info and avoids calling the action
            serviceCollection.RemoveWhere(x => x.ServiceType == typeof(IActionInvokerFactory));
            serviceCollection.AddSingleton<IActionInvokerFactory, RouteTesterActionInvokerFactory>();
            */

            /*
            // Remove authorization
            // TODO: Should this be an option?
            serviceCollection.RemoveWhere(x => x.ImplementationType == typeof(AuthorizationApplicationModelProvider));
            */
        }

        #endregion

    }

}
