namespace MvcRouteTester.AspNetCore.Infrastructure;

internal class XunitTestFramework : ITestFramework
{
    public bool IsAvailable => AppDomain.CurrentDomain.GetAssemblies().Any(x => x.GetName().Name == "xunit.assert");

    public void Equal<T>(T? expected, T? actual)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => x.GetName().Name == "xunit.assert");
            if (assembly == null)
            {
                throw new InvalidOperationException("TestFramework assembly not found");
            }

            // TODO: Possibly replace this with a call to "Assert.Equal<T>(T expected, T actual)"
            //       But requires tricky reflection so leave it like this for now
            var type = assembly.GetType("Xunit.Sdk.EqualException");
            if (type == null)
            {
                throw new InvalidOperationException("TestFramework exception type not found");
            }

            var ex = (Exception)Activator.CreateInstance(type, args: new[] { (object?)expected, (object?)actual })!;
            throw ex;
        }
    }
}
