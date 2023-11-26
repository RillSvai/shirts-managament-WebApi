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
        _ = await _webApiExecuter.InvokePost("shirt", shirt);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        Shirt? shirt = await _webApiExecuter.InvokeGet<Shirt>($"shirt/{id}");
        if (shirt is null)
        {
            return NotFound();
        }

        return View(shirt);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Shirt shirt)
    {
        await Task.Delay(1);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return RedirectToAction(nameof(Index));
    }
}