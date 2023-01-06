using MvcRouteTester.AspNetCore.Internal;

namespace MvcRouteTester.AspNetCore.Builders;

public interface IRequestBuilder : IFluentInterface
{
    IRequestBuilder WithMethod(HttpMethod method);

    IRequestBuilder WithPathAndQuery(string pathAndQuery);

    IRequestBuilder WithFormContent(IDictionary<string, string> content);

    IRequestBuilder WithJsonContent(object content);
}
