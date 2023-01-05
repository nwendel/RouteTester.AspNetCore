using System.Net.Http.Json;
using Microsoft.AspNetCore.TestHost;

namespace MvcRouteTester.AspNetCore.Builders;

public class RouteTesterRequest : IRequestBuilder
{
    private HttpMethod _method = HttpMethod.Get;
    private string _pathAndQuery = "/";
    private IDictionary<string, string> _formData = new Dictionary<string, string>();
    private object? _jsonData;

    public IRequestBuilder WithMethod(HttpMethod method)
    {
        GuardAgainst.Null(method);

        _method = method;
        return this;
    }

    public IRequestBuilder WithPathAndQuery(string pathAndQuery)
    {
        // TODO: NullOrWhiteSpace?
        GuardAgainst.Null(pathAndQuery);

        _pathAndQuery = pathAndQuery;
        return this;
    }

    public IRequestBuilder WithFormData(IDictionary<string, string> formData)
    {
        GuardAgainst.Null(formData);

        _formData = formData;
        return this;
    }

    public IRequestBuilder WithJsonData(object jsonData)
    {
        GuardAgainst.Null(jsonData);

        _jsonData = jsonData;
        return this;
    }

    public async Task<HttpResponseMessage> ExecuteAsync(TestServer server)
    {
        GuardAgainst.Null(server);

        var client = server.CreateClient();
        using var requestMessage = new HttpRequestMessage(_method, _pathAndQuery);

        // REVIEW: Only with POST method?
        if (_method == HttpMethod.Post)
        {
            if (_formData.Any())
            {
                requestMessage.Content = new FormUrlEncodedContent(_formData);
            }
            else if (_jsonData != null)
            {
                requestMessage.Content = JsonContent.Create(_jsonData, _jsonData.GetType());
            }
        }

        var responseMessage = await client.SendAsync(requestMessage);
        return responseMessage;
    }
}
