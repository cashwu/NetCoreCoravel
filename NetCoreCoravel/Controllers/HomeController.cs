using Microsoft.AspNetCore.Mvc;

namespace NetCoreCoravel.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return Ok(123);
    }
}