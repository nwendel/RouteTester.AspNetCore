using Microsoft.AspNetCore.Mvc;

namespace TestApplication.Controllers;

public class ParameterController : Controller
{
    [HttpGet("parameter/same-name-with-string")]
    public IActionResult SameName(string parameter1, string parameter2)
    {
        throw new NotImplementedException();
    }

    [HttpGet("parameter/same-name-with-int")]
    public IActionResult SameName(string parameter1, int parameter2)
    {
        throw new NotImplementedException();
    }

    [HttpGet("parameter/query-string-parameter")]
    public IActionResult QueryStringParameter(string parameter)
    {
        throw new NotImplementedException();
    }
}
