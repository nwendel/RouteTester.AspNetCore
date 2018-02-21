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
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MvcRouteTester.AspNetCore.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class RouteTesterActionInvoker : IActionInvoker
    {

        #region Dependencies

        private readonly ActionContext _actionContext;
        private readonly IList<IValueProviderFactory> _valueProviderFactories;
        private readonly ContentResultExecutor _contentResultExecutor;
        private readonly ControllerActionInvokerCache _controllerActionInvokerCache;
        private readonly ActionInvokerFactory _actionInvokerFactory;
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
            ContentResultExecutor contentResultExecutor,
            ControllerActionInvokerCache controllerActionInvokerCache,
            ActionInvokerFactory actionInvokerFactory,
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
                ActionInfo = new ActionInfo(actionMethodInfo),
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
            var actionMethodExecutor = cacheEntry.GetActionMethodExecutor();
            var arguments = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            var controllerActionInvoker = (ControllerActionInvoker)_actionInvokerFactory.CreateInvoker(_actionContext);
            var binder = cacheEntry.ControllerBinderDelegate;
            binder?.Invoke(controllerContext, new object(), arguments).Wait();
            var orderedArguments = controllerActionInvoker.PrepareArguments(arguments, actionMethodExecutor);
            return orderedArguments;
        }

        #endregion

    }

}
