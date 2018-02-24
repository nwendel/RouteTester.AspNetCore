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
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Xunit;
using MvcRouteTester.AspNetCore.Internal;

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

            var validParameterName = _expectedActionInvokeInfo
                .ActionMethodInfo
                .GetParameters()
                .Any(x => x.Name == parameterName);
            if (!validParameterName)
            {
                throw new ArgumentException($"Invalid parameter name {parameterName}", nameof(parameterName));
            }

            var parameterAssert = new ParameterAssert(parameterName, x => { action.Invoke((T)x); });
            _parameterAsserts.Add(parameterAssert);
            return this;
        }

        #endregion

        #region Assert Expected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseMessage"></param>
        public void AssertExpected(HttpResponseMessage responseMessage)
        {
            responseMessage.EnsureSuccessStatusCode();

            var key = responseMessage.Content.ReadAsStringAsync().Result;
            var actualActionInvokeInfo = _actionInvokeInfoCache[key];
            _actionInvokeInfoCache.Remove(key);

            AssertExpectedMethodInfo(actualActionInvokeInfo);
            AssertExpectedParameterValues(actualActionInvokeInfo);
            AssertExpectedParameterAsserts(actualActionInvokeInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualActionInvokeInfo"></param>
        private void AssertExpectedMethodInfo(ActualActionInvokeInfo actualActionInvokeInfo)
        {
            // TODO: Remove Xunit usage
            Assert.Equal(_expectedActionInvokeInfo.ActionMethodInfo, actualActionInvokeInfo.ActionMethodInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualActionInvokeInfo"></param>
        private void AssertExpectedParameterValues(ActualActionInvokeInfo actualActionInvokeInfo)
        {
            // TODO: Remove Xunit usage
            for (var ix = 0; ix < _expectedActionInvokeInfo.ActionMethodInfo.GetParameters().Length; ix++)
            {
                switch (_expectedActionInvokeInfo.ArgumentAssertKinds[ix])
                {
                    case ArgumentAssertKind.Value:
                        Assert.Equal(
                            _expectedActionInvokeInfo.Arguments[ix],
                            actualActionInvokeInfo.Arguments[ix]);
                        break;
                    case ArgumentAssertKind.Any:
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualActionInvokeInfo"></param>
        private void AssertExpectedParameterAsserts(ActualActionInvokeInfo actualActionInvokeInfo)
        {
            // TODO: Remove Xunit usage
            foreach (var parameterAssert in _parameterAsserts)
            {
                var index = actualActionInvokeInfo
                    .ActionMethodInfo
                    .GetParameters()
                    .Select((p, ix) => new { Parameter = p, Index = ix })
                    .Single(p => p.Parameter.Name == parameterAssert.Name)
                    .Index;
                var value = actualActionInvokeInfo.Arguments[index];
                parameterAssert.Action(value);
            }
        }

        #endregion

    }

}
