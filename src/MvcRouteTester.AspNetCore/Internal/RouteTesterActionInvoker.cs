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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
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
        private readonly JsonResultExecutor _jsonResultExecutor;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="valueProviderFactories"></param>
        /// <param name="jsonResultExecutor"></param>
        public RouteTesterActionInvoker(
            ActionContext actionContext, 
            IList<IValueProviderFactory> valueProviderFactories, 
            JsonResultExecutor jsonResultExecutor)
        {
            _actionContext = actionContext;
            _valueProviderFactories = valueProviderFactories;
            _jsonResultExecutor = jsonResultExecutor;
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

            var actionInvokeInfo = new ActionInvokeInfo
            {
                ControllerTypeAssemblyQualifiedName = controllerContext.ActionDescriptor.ControllerTypeInfo.AssemblyQualifiedName
            };

            var jsonResult = new JsonResult(actionInvokeInfo);
            await _jsonResultExecutor.ExecuteAsync(_actionContext, jsonResult);
        }

        #endregion

    }

}
