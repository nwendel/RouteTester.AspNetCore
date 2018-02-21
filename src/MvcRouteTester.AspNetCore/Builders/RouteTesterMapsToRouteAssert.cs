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
using Newtonsoft.Json;
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

        #region Fields

        private ExpectedActionInvokeInfo _expectedActionInvokeInfo;

        #endregion

        #region Parse Action Call Expression

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionCallExpression"></param>
        public void ParseActionCallExpression(LambdaExpression actionCallExpression)
        {
            var parser = new RouteExpressionParser();
            _expectedActionInvokeInfo = parser.Parse(actionCallExpression);
        }

        #endregion

        #region For Parameter

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public IRouteAssertMapsToBuilder ForParameter<T>(string name, Action<T> action)
        {
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

            var json = responseMessage.Content.ReadAsStringAsync().Result;
            var actualActionInvokeInfo = JsonConvert.DeserializeObject<ActualActionInvokeInfo>(json);

            // TODO: Rewrite!
            Assert.Equal(
                _expectedActionInvokeInfo.ActionInfo.ControllerTypeNameInfo.AssemblyQualifiedName, 
                actualActionInvokeInfo.ActionInfo.ControllerTypeNameInfo.AssemblyQualifiedName);
            Assert.Equal(
                _expectedActionInvokeInfo.ActionInfo.ActionMethodName, 
                actualActionInvokeInfo.ActionInfo.ActionMethodName);
            Assert.Equal(
                _expectedActionInvokeInfo.ActionInfo.ParameterInfos.Length, 
                actualActionInvokeInfo.ActionInfo.ParameterInfos.Length);
            for (var ix = 0; ix < _expectedActionInvokeInfo.ActionInfo.ParameterInfos.Length; ix++)
            {
                Assert.Equal(
                    _expectedActionInvokeInfo.ActionInfo.ParameterInfos[ix].TypeNameInfo.AssemblyQualifiedName, 
                    actualActionInvokeInfo.ActionInfo.ParameterInfos[ix].TypeNameInfo.AssemblyQualifiedName);
                switch(_expectedActionInvokeInfo.ArgumentAssertKinds[ix])
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

        #endregion

    }

}
