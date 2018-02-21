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

namespace MvcRouteTester.AspNetCore.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ActualActionInvokeInfoCache
    {

        #region Fields

        private readonly Dictionary<string, ActualActionInvokeInfo> _cache = new Dictionary<string, ActualActionInvokeInfo>();

        #endregion

        #region Add

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="actualActionInvokeInfo"></param>
        public void Add(string key, ActualActionInvokeInfo actualActionInvokeInfo)
        {
            lock (_cache)
            {
                _cache.Add(key, actualActionInvokeInfo);
            }
        }

        #endregion

        #region Indexer

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActualActionInvokeInfo this[string key]
        {
            get
            {
                lock(_cache)
                {
                    return _cache[key];
                }
            }
        }

        #endregion

        #region Remove

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            lock (_cache)
            {
                _cache.Remove(key);
            }
        }

        #endregion

    }

}
