using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace MvcRouteTester.AspNetCore.Infrastructure;

internal static class ObjectExtensions
{
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
