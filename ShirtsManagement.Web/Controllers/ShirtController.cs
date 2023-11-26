using Microsoft.AspNetCore.Mvc;
using ShirtsManagement.Web.Data;
using ShirtsManagement.Web.Models;

namespace ShirtsManagement.Web.Controllers;

public class ShirtController : Controller
{
    private readonly IWebApiExecuter _webApiExecuter;

    public ShirtController(IWebApiExecuter webApiExecuter)
    {
        _webApiExecuter = webApiExecuter;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _webApiExecuter.InvokeGet<IEnumerable<Shirt>>("shirt"));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Shirt shirt)
    {
        if (!ModelState.IsValid)
        {
            return View(shirt);
        }
        await Task.Delay(1);
        return RedirectToAction(nameof(Index));
    }
}