using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using MvcRouteTester.AspNetCore.Internal;

namespace MvcRouteTester.AspNetCore.Builders
{
    public class RouteTesterRequest : IRequestBuilder
    {
        private HttpMethod _method = HttpMethod.Get;
        private string _pathAndQuery = "/";
        private IDictionary<string, string> _formData = new Dictionary<string, string>();
        private object? _jsonData;

        public IRequestBuilder WithMethod(HttpMethod method)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            _method = method;
            return this;
        }

        public IRequestBuilder WithPathAndQuery(string pathAndQuery)
        {
            if (pathAndQuery == null)
            {
                throw new ArgumentNullException(nameof(pathAndQuery));
            }

            _pathAndQuery = pathAndQuery;
            return this;
        }

        public IRequestBuilder WithFormData(IDictionary<string, string> formData)
        {
            if (formData == null)
            {
                throw new ArgumentNullException(nameof(formData));
            }

            _formData = formData;
            return this;
        }

        public IRequestBuilder WithJsonData(object jsonData)
        {
            if (jsonData == null)
            {
                throw new ArgumentNullException(nameof(jsonData));
            }

            _jsonData = jsonData;
            return this;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(TestServer server)
        {
            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }

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
                    requestMessage.Content = _jsonData.ToHttpContent();
                }
            }

            var responseMessage = await client.SendAsync(requestMessage);
            return responseMessage;
        }
    }
}
