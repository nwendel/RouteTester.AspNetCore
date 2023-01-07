namespace MvcRouteTester.AspNetCore;

// TODO: Workaround for now, when I understand why the InvalidOperationException "The TestServer constructor was not called with a IWebHostBuilder so IWebHost is not available." is thrown
//       This can be removed
public interface IWebApplicationFactory
{
    IServiceProvider Services { get; }

    HttpClient CreateClient();
}
