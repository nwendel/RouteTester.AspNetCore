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
using System.Linq;
using System.Reflection;
using System.Text;

namespace MvcRouteTester.AspNetCore.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public static class MethodInfoExtensions
    {

        #region Get Action Text

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getTypeName"></param>
        /// <returns></returns>
        public static string GetActionText(this MethodInfo self, Func<Type, string> getTypeName)
        {
            if (getTypeName == null)
            {
                throw new ArgumentNullException(nameof(getTypeName));
            }

            var builder = new StringBuilder();
            builder.Append(getTypeName(self.ReflectedType));
            builder.Append(".");
            builder.Append(self.Name);
            builder.Append("(");
            builder.Append(string.Join(",", self
                .GetParameters()
                .Select(p => getTypeName(p.ParameterType))));
            builder.Append(")");
            return builder.ToString();
        }

        #endregion
        
    }

}
