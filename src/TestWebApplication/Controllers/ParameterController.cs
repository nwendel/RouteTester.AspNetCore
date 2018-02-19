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
using Microsoft.AspNetCore.Mvc;

namespace TestWebApplication.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class ParameterController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        /// <returns></returns>
        [HttpGet("parameter/same-name-with-string")]
        public IActionResult SameName(string parameter1, string parameter2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        /// <returns></returns>
        [HttpGet("parameter/same-name-with-int")]
        public IActionResult SameName(string parameter1, int parameter2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpGet("parameter/query-string-parameter")]
        public IActionResult QueryStringParameter(string parameter)
        {
            throw new NotImplementedException();
        }

    }

}
