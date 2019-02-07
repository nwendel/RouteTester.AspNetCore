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
using System.Collections.Generic;
using System.Net.Http;
using MvcRouteTester.AspNetCore.Internal;

namespace MvcRouteTester.AspNetCore.Builders
{

    /// <summary>
    /// 
    /// </summary>
    public interface IRequestBuilder : IFluentInterface
    {

        #region With Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        IRequestBuilder WithMethod(HttpMethod method);

        #endregion

        #region With Path And Query

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathAndQuery"></param>
        /// <returns></returns>
        IRequestBuilder WithPathAndQuery(string pathAndQuery);

        #endregion

        #region With Form Data

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IRequestBuilder WithFormData(IDictionary<string, string> formData);

        #endregion

    }

}
