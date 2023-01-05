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
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MvcRouteTester.AspNetCore.Internal;
using Xunit;

namespace MvcRouteTester.AspNetCore.Builders
{

    /// <summary>
    /// 
    /// </summary>
    public class RouteTesterMapsToRouteAssert :
        IRouteAssertMapsToBuilder,
        IRouteAssert
    {

        #region Dependencies

        private readonly ActualActionInvokeInfoCache _actionInvokeInfoCache;
        private readonly RouteExpressionParser _routeExpressionParser;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        #endregion

        #region Fields

        private ExpectedActionInvokeInfo _expectedActionInvokeInfo;
        private readonly List<ParameterAssert> _parameterAsserts = new List<ParameterAssert>();

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionInvokeInfoCache"></param>
        /// <param name="routeExpressionParser"></param>
        /// <param name="actionDescriptorCollectionProvider"></param>
        public RouteTesterMapsToRouteAssert(
            ActualActionInvokeInfoCache actionInvokeInfoCache,
            RouteExpressionParser routeExpressionParser,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionInvokeInfoCache = actionInvokeInfoCache;
            _routeExpressionParser = routeExpressionParser;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        #endregion

        #region Parse Action Call Expression

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionCallExpression"></param>
        public void ParseActionCallExpression(LambdaExpression actionCallExpression)
        {
            _expectedActionInvokeInfo = _routeExpressionParser.Parse(actionCallExpression);

            var isActionMethod = _actionDescriptorCollectionProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .Any(x => x.MethodInfo == _expectedActionInvokeInfo.ActionMethodInfo);
            if (isActionMethod)
            {
                return;
            }
            var actionText = _expectedActionInvokeInfo.ActionMethodInfo.GetActionText(x => x.Name);
            throw new ArgumentException($"Method {actionText} is not a valid controller action", nameof(actionCallExpression));
        }

        #endregion

        #region For Parameter

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public IRouteAssertMapsToBuilder ForParameter<T>(string parameterName, Action<T> action)
        {
            if (parameterName == null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var expectedParameter = _expectedActionInvokeInfo
                .ActionMethodInfo
                .GetParameters()
                .SingleOrDefault(x => x.Name == parameterName);
            if (expectedParameter == null)
            {
                throw new ArgumentException($"Invalid parameter name {parameterName}", nameof(parameterName));
            }
            if (expectedParameter.ParameterType != typeof(T))
            {
                throw new ArgumentException($"Invalid parameter type expected {expectedParameter.ParameterType.Name} but call was {typeof(T).Name}", nameof(T));
            }

            var parameterAssert = new ParameterAssert(parameterName, x => { action.Invoke((T)x); });
            _parameterAsserts.Add(parameterAssert);
            return this;
        }

        #endregion

        #region Assert Expected Async

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseMessage"></param>
        public async Task AssertExpectedAsync(HttpResponseMessage responseMessage)
        {
            responseMessage.EnsureSuccessStatusCode();

            var key = await responseMessage.Content.ReadAsStringAsync();
            var actualActionInvokeInfo = _actionInvokeInfoCache[key];
            _actionInvokeInfoCache.Remove(key);

            AssertExpectedMethodInfo(actualActionInvokeInfo.ActionMethodInfo);
            AssertExpectedParameterValues(actualActionInvokeInfo);
            AssertExpectedParameterAsserts(actualActionInvokeInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualActionMethodInfo"></param>
        private void AssertExpectedMethodInfo(MethodInfo actualActionMethodInfo)
        {
            // TODO: Remove Xunit usage
            Assert.Equal(_expectedActionInvokeInfo.ActionMethodInfo, actualActionMethodInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualActionInvokeInfo"></param>
        private void AssertExpectedParameterValues(ActualActionInvokeInfo actualActionInvokeInfo)
        {
            var parameterNames = _expectedActionInvokeInfo.ActionMethodInfo.GetParameters()
                .Select(x => x.Name)
                .ToList();
            foreach (var parameterName in parameterNames)
            {
                switch (_expectedActionInvokeInfo.ArgumentAssertKinds[parameterName])
                {
                    case ArgumentAssertKind.Value:
                        // TODO: Remove Xunit usage
                        Assert.Equal(
                            _expectedActionInvokeInfo.Arguments[parameterName],
                            actualActionInvokeInfo.Arguments.TryGetValue(parameterName, out var actual) ? actual : null);
                        break;
                    case ArgumentAssertKind.Any:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualActionInvokeInfo"></param>
        private void AssertExpectedParameterAsserts(ActualActionInvokeInfo actualActionInvokeInfo)
        {
            foreach (var parameterAssert in _parameterAsserts)
            {
                var value = actualActionInvokeInfo.Arguments[parameterAssert.Name];
                parameterAssert.Action(value);
            }
        }

        #endregion

    }

}
