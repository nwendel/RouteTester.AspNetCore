using TestApplication.Model;

namespace TestApplication.Controllers;

public class PostController : Controller
{
    [HttpPost("simple-attribute-route-post")]
    public IActionResult SimpleAttributeRoute()
    {
        throw new NotImplementedException();
    }

    [HttpPost("post-with-person")]
    public IActionResult WithPerson(Person person)
    {
        throw new NotImplementedException();
    }

    [HttpPost("post-with-json-person")]
    public IActionResult WithJsonPerson([FromBody] Person person)
    {
        throw new NotImplementedException();
    }
}
