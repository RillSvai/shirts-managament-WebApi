using Microsoft.AspNetCore.Mvc;
using ShirtsManagement.Web.Models;

namespace ShirtsManagement.Web.Controllers;

public class ShirtController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}