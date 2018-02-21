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
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MvcRouteTester.AspNetCore.Builders
{

    /// <summary>
    /// 
    /// </summary>
    public class RouteTesterRouteAssert : 
        IRouteAssertBuilder,
        IRouteAssert
    {

        #region Dependencies

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public RouteTesterRouteAssert(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Maps To

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        /// <param name="actionCallExpression"></param>
        /// <returns></returns>
        public IRouteAssertMapsToBuilder MapsTo<TController>(Expression<Func<TController, IActionResult>> actionCallExpression) 
            where TController : Controller
        {
            return MapsToCore(actionCallExpression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        /// <param name="actionCallExpression"></param>
        /// <returns></returns>
        public IRouteAssertMapsToBuilder MapsTo<TController>(Expression<Func<TController, Task<IActionResult>>> actionCallExpression) 
            where TController : Controller
        {
            return MapsToCore(actionCallExpression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionCallExpression"></param>
        /// <returns></returns>
        private IRouteAssertMapsToBuilder MapsToCore(LambdaExpression actionCallExpression)
        {
            var builder = _serviceProvider.GetRequiredService<RouteTesterMapsToRouteAssert>();
            builder.ParseActionCallExpression(actionCallExpression);
            _routeAssert = builder;
            return builder;
        }

        #endregion

        #region Not Found

        /// <summary>
        /// 
        /// </summary>
        public void NotFound()
        {
            var builder = _serviceProvider.GetRequiredService<RouteTesterNotFoundRouteAssert>();
            _routeAssert = builder;
        }

        #endregion

        #region Assert Expected

        private IRouteAssert _routeAssert;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseMessage"></param>
        public void AssertExpected(HttpResponseMessage responseMessage)
        {
            _routeAssert.AssertExpected(responseMessage);
        }

        #endregion

    }

}
