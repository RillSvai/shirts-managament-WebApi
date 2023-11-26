using Microsoft.AspNetCore.Mvc;

namespace ShirtsManagament.Web.Controllers;

public class ShirtController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}