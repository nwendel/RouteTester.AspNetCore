using MvcRouteTester.AspNetCore.Internal;

namespace MvcRouteTester.AspNetCore.Builders
{
    public interface IRequestBuilder : IFluentInterface
    {
        IRequestBuilder WithMethod(HttpMethod method);

        IRequestBuilder WithPathAndQuery(string pathAndQuery);

        IRequestBuilder WithFormData(IDictionary<string, string> formData);

        IRequestBuilder WithJsonData(object jsonData);
    }
}
