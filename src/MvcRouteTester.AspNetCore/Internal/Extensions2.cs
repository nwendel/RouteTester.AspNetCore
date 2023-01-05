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
    public static class Extensions2
    {
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

        public static HttpContent? ToHttpContent(this object data)
        {
            if (data == null)
            {
                return null;
            }

            var ms = new MemoryStream();
            SerializeJsonIntoStream(data, ms);
            ms.Seek(0, SeekOrigin.Begin);
            var httpContent = new StreamContent(ms);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpContent;
        }

        private static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true);
            using var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None };

            var js = new JsonSerializer();
            js.Serialize(jtw, value);
            jtw.Flush();
        }
    }
}
