using System.Net.Http.Json;
using Microsoft.AspNetCore.TestHost;

namespace MvcRouteTester.AspNetCore.Builders;

public sealed class RouteTesterRequest : IRequestBuilder, IDisposable
{
    private HttpMethod _method = HttpMethod.Get;
    private string _pathAndQuery = "/";
    private HttpContent? _content;

    public IRequestBuilder WithMethod(HttpMethod method)
    {
        GuardAgainst.Null(method);

        _method = method;
        return this;
    }

    public IRequestBuilder WithPathAndQuery(string pathAndQuery)
    {
        GuardAgainst.NullOrWhiteSpace(pathAndQuery);

        _pathAndQuery = pathAndQuery;
        return this;
    }

    public IRequestBuilder WithFormContent(IDictionary<string, string> content)
    {
        GuardAgainst.Null(content);

        _content = new FormUrlEncodedContent(content);
        return this;
    }

    public IRequestBuilder WithJsonContent(object content)
    {
        GuardAgainst.Null(content);

        _content = JsonContent.Create(content, content.GetType());
        return this;
    }

    public async Task<HttpResponseMessage> ExecuteAsync(TestServer server)
    {
        GuardAgainst.Null(server);

        var client = server.CreateClient();
        using var requestMessage = new HttpRequestMessage(_method, _pathAndQuery);

        // REVIEW: Only with POST method?
        if (_method == HttpMethod.Post && _content != null)
        {
            requestMessage.Content = _content;
        }

        var responseMessage = await client.SendAsync(requestMessage);
        return responseMessage;
    }

    public async Task<HttpResponseMessage> ExecuteAsync(IWebApplicationFactory factory)
    {
        GuardAgainst.Null(factory);

        var client = factory.CreateClient();
        using var requestMessage = new HttpRequestMessage(_method, _pathAndQuery);

        // REVIEW: Only with POST method?
        if (_method == HttpMethod.Post && _content != null)
        {
            requestMessage.Content = _content;
        }

        var responseMessage = await client.SendAsync(requestMessage);
        return responseMessage;
    }

    public void Dispose()
    {
        _content?.Dispose();
        _content = null;
    }
}
