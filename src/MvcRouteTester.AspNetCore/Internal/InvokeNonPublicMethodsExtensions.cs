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
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Internal;

namespace MvcRouteTester.AspNetCore.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public static class InvokeNonPublicMethodsExtensions
    {

        #region Get Action Method Executor

        private static readonly PropertyInfo _actionMethodExecutorPropertyInfo = typeof(ControllerActionInvokerCacheEntry).GetProperty("ActionMethodExecutor", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static object GetActionMethodExecutor(this ControllerActionInvokerCacheEntry self)
        {
            var value = _actionMethodExecutorPropertyInfo.GetValue(self);
            return value;
        }

        #endregion

        #region prepare Arguments

        private static readonly MethodInfo _prepareArgumentsMethodInfo = typeof(ControllerActionInvoker).GetMethod("PrepareArguments", BindingFlags.NonPublic | BindingFlags.Static);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="arguments"></param>
        /// <param name="actionMethodExecutor"></param>
        /// <returns></returns>
        public static object[] PrepareArguments(this ControllerActionInvoker self, Dictionary<string, object> arguments, object actionMethodExecutor)
        {
            return (object[])_prepareArgumentsMethodInfo.Invoke(self, new[] { arguments, actionMethodExecutor });
        }

        #endregion

    }

}
