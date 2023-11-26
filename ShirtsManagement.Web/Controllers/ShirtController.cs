using Microsoft.AspNetCore.Mvc;

namespace ShirtsManagement.Web.Controllers;

public class ShirtController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}