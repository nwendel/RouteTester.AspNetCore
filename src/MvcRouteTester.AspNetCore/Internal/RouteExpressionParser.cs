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
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MvcRouteTester.AspNetCore.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class RouteExpressionParser
    {

        #region Fields

        private readonly MethodInfo _anyMethod = typeof(Args).GetMethod(nameof(Args.Any));

        #endregion

        #region Parse

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionCallExpression"></param>
        /// <returns></returns>
        public ExpectedActionInvokeInfo Parse(LambdaExpression actionCallExpression)
        {
            var methodCallExpression = GetInstanceMethodCallExpression(actionCallExpression);
            var methodInfo = methodCallExpression.Method;

            var arguments = methodCallExpression.Arguments
                .Select(x => Expression.Lambda(x).Compile().DynamicInvoke())
                .ToArray();
            var argumentAssertKinds = methodCallExpression.Arguments
                .Select(x =>
                {
                    if (x is not MethodCallExpression call)
                    {
                        return ArgumentAssertKind.Value;
                    }

                    var anyMethod = _anyMethod.MakeGenericMethod(call.Method.ReturnType);
                    return call.Method == anyMethod
                        ? ArgumentAssertKind.Any
                        : ArgumentAssertKind.Value;
                })
                .ToArray();

            var result = new ExpectedActionInvokeInfo
            {
                ActionMethodInfo = methodInfo,
                Arguments = methodInfo.GetParameters().Select(x => x.Name).Zip(arguments).ToDictionary(k => k.First, v => v.Second),
                ArgumentAssertKinds = methodInfo.GetParameters().Select(x => x.Name).Zip(argumentAssertKinds).ToDictionary(k => k.First, v => v.Second)
            };
            return result;
        }

        #endregion

        #region Get Instance Method Call Expression

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionCallExpression"></param>
        /// <returns></returns>
        private static MethodCallExpression GetInstanceMethodCallExpression(LambdaExpression actionCallExpression)
        {
            if (actionCallExpression.Body is not MethodCallExpression methodCallExpression)
            {
                throw new ArgumentException("Not a method call expression", nameof(actionCallExpression));
            }
            var objectInstance = methodCallExpression.Object;
            if (objectInstance == null)
            {
                throw new ArgumentException("Not an instance method call expression", nameof(actionCallExpression));
            }

            return methodCallExpression;
        }

        #endregion

    }

}
