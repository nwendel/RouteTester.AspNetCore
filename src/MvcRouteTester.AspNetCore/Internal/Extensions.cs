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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace MvcRouteTester.AspNetCore.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {

        #region ICollection / Remove Where 

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        public static void RemoveWhere<TItem>(this ICollection<TItem> self, Func<TItem, bool> predicate)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var remove = self.Where(predicate).ToList();
            self.RemoveRange(remove);
        }

        #endregion

        #region ICollection / Remove Range

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="self"></param>
        /// <param name="items"></param>
        public static void RemoveRange<TItem>(this ICollection<TItem> self, IEnumerable<TItem> items)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (var item in items)
            {
                self.Remove(item);
            }
        }

        #endregion

        #region Object / Get HttpContent

        public static HttpContent ToHttpContent(this object data)
        {
            HttpContent httpContent = null;

            if (data != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(data, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        private static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        #endregion
    }

}
