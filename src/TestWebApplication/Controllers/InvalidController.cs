using Microsoft.AspNetCore.Mvc;

namespace TestWebApplication.Controllers;

public class InvalidController : Controller
{
    [HttpGet("invalid")]
    public IActionResult Default()
    {
        throw new NotImplementedException();
    }

    [HttpGet("invalid/static")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1204:Static elements should appear before instance elements", Justification = "Not valid for contrller actions")]
    public static IActionResult Static()
    {
        throw new NotImplementedException();
    }

    [NonAction]
    public IActionResult NonAction()
    {
        throw new NotImplementedException();
    }
}
