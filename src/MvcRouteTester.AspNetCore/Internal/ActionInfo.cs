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
using System.Linq;
using System.Reflection;
using System.Text;

namespace MvcRouteTester.AspNetCore.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ActionInfo
    {

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        public ActionInfo(MethodInfo methodInfo)
        {
            if(methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            ControllerTypeNameInfo = new TypeNameInfo(methodInfo.ReflectedType);
            ActionMethodName = methodInfo.Name;
            ParameterInfos = methodInfo.GetParameters()
                .Select((p, ix) => new ActionParameterInfo(ix, p))
                .ToArray();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public TypeNameInfo ControllerTypeNameInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        public string ActionMethodName { get; }

        /// <summary>
        /// 
        /// </summary>
        public ActionParameterInfo[] ParameterInfos { get; }

        #endregion

        #region Get Text

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getTypeName"></param>
        /// <returns></returns>
        public string GetText(Func<TypeNameInfo, string> getTypeName)
        {
            if (getTypeName == null)
            {
                throw new ArgumentNullException(nameof(getTypeName));
            }

            var builder = new StringBuilder();
            builder.Append(getTypeName(ControllerTypeNameInfo));
            builder.Append(".");
            builder.Append(ActionMethodName);
            builder.Append("(");
            builder.Append(string.Join(",", ParameterInfos
                .Select(p => getTypeName(p.TypeNameInfo))));
            builder.Append(")");
            return builder.ToString();
        }

        #endregion
        
    }

}
