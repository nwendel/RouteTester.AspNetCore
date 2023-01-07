using MvcRouteTester.AspNetCore.Internal;
using TestApplication.Controllers;
using Xunit;

namespace MvcRouteTester.AspNetCore.Tests.Internal;

public class MethodInfoExtensionsTests
{
    [Fact]
    public void ThrowsOnGetTextNullGetTypeName()
    {
        var tested = typeof(ParameterController)
            .GetMethod(nameof(ParameterController.QueryStringParameter));

        Assert.Throws<ArgumentNullException>("getTypeName", () => tested!.GetActionText(null!));
    }
}
