using RouteTester.AspNetCore.Infrastructure;

namespace RouteTester.AspNetCore.Tests.Conventions.Builders;

public class BuilderConventionsTests
{
    [Fact]
    public void BuildersHidesObjectMembers()
    {
        Convention.ForTypes(
            s => s
                .FromAssemblyContaining<IFluentInterface>()
                .Where(t => t.Type.Namespace == "RouteTester.AspNetCore.Builders" && t.Type.IsInterface),
            a => a.Assert((type, context) =>
                {
                    if (type.IsAssignableTo(typeof(IFluentInterface)))
                    {
                        return;
                    }

                    context.Fail(type, $" must implement interface {nameof(IFluentInterface)}");
                }));
    }
}
