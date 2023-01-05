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

using System.Collections.Generic;
using System.Reflection;

namespace MvcRouteTester.AspNetCore.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ActualActionInvokeInfo
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public MethodInfo ActionMethodInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<string, object> Arguments { get; set; }

        #endregion

    }

}
