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
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcRouteTester.AspNetCore.Internal.Wrappers;

namespace MvcRouteTester.AspNetCore.Internal
{

    /// <summary>
    /// 
    /// </summary>F
    public class RouteTesterActionInvoker : IActionInvoker
    {

        #region Dependencies

        private readonly ActionContext _actionContext;
        private readonly IList<IValueProviderFactory> _valueProviderFactories;
        private readonly IActionResultExecutor<ContentResult> _contentResultExecutor;
        private readonly ControllerActionInvokerCacheWrapper _controllerActionInvokerCache;
        private readonly ActionInvokerFactoryWrapper _actionInvokerFactory;
        private readonly ActualActionInvokeInfoCache _actionInvokeInfoCache;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="valueProviderFactories"></param>
        /// <param name="contentResultExecutor"></param>
        /// <param name="controllerActionInvokerCache"></param>
        /// <param name="actionInvokerFactory"></param>
        /// <param name="actionInvokeInfoCache"></param>
        public RouteTesterActionInvoker(
            ActionContext actionContext,
            IList<IValueProviderFactory> valueProviderFactories,
            IActionResultExecutor<ContentResult> contentResultExecutor,
            ControllerActionInvokerCacheWrapper controllerActionInvokerCache,
            ActionInvokerFactoryWrapper actionInvokerFactory,
            ActualActionInvokeInfoCache actionInvokeInfoCache)
        {
            _actionContext = actionContext;
            _valueProviderFactories = valueProviderFactories;
            _contentResultExecutor = contentResultExecutor;
            _controllerActionInvokerCache = controllerActionInvokerCache;
            _actionInvokerFactory = actionInvokerFactory;
            _actionInvokeInfoCache = actionInvokeInfoCache;
        }

        #endregion

        #region Invoke Async

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task InvokeAsync()
        {
            var controllerContext = new ControllerContext(_actionContext)
            {
                ValueProviderFactories = _valueProviderFactories
            };
            var controllerActionDescriptor = controllerContext.ActionDescriptor;
            var actionMethodInfo = controllerActionDescriptor.MethodInfo;
            var arguments = BindArguments(controllerContext);

            var actionInvokeInfo = new ActualActionInvokeInfo
            {
                ActionMethodInfo = actionMethodInfo,
                Arguments = arguments
            };

            var key = Guid.NewGuid().ToString();
            _actionInvokeInfoCache.Add(key, actionInvokeInfo);

            var contentResult = new ContentResult
            {
                Content = key
            };
            await _contentResultExecutor.ExecuteAsync(_actionContext, contentResult);
        }

        #endregion

        #region Bind Arguments

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        private object[] BindArguments(ControllerContext controllerContext)
        {
            (var cacheEntry, var _) = _controllerActionInvokerCache.GetCachedResult(controllerContext);
            var actionMethodExecutor = cacheEntry.GetObjectMethodExecutor();
            var controllerActionInvoker = _actionInvokerFactory.CreateInvoker(_actionContext);
            var binder = cacheEntry.ControllerBinderDelegate;

            var arguments = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            binder?.Invoke(controllerContext, new object(), arguments).Wait();
            var orderedArguments = controllerActionInvoker.PrepareArguments(arguments, actionMethodExecutor);

            return orderedArguments;
        }

        #endregion

    }

}
