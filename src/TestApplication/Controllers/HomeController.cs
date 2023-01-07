namespace TestApplication.Controllers;

public class HomeController : Controller
{
    [HttpGet("simple-attribute-route")]
    public IActionResult SimpleAttributeRoute()
    {
        throw new NotImplementedException();
    }

    [HttpGet("simple-attribute-route-async")]
    public Task<IActionResult> SimpleAttributeRouteAsync()
    {
        throw new NotImplementedException();
    }
}
