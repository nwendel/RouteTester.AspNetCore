using RouteTester.AspNetCore.Infrastructure;

namespace RouteTester.AspNetCore.Builders;

public interface IRequestBuilder : IFluentInterface
{
    IRequestBuilder WithMethod(HttpMethod method);

    IRequestBuilder WithPathAndQuery(string pathAndQuery);

    IRequestBuilder WithFormContent(IDictionary<string, string> content);

    IRequestBuilder WithJsonContent(object content);
}
