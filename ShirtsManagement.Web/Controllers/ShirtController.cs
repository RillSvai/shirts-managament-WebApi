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
        try
        {
            _ = await _webApiExecuter.InvokePost("shirt", shirt);
        }
        catch (WebApiException ex)
        {
            HandleWebApiException(ex);
            return View(shirt);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        try
        {
            Shirt? shirt = await _webApiExecuter.InvokeGet<Shirt>($"shirt/{id}");
            if (shirt is not null)
            {
                return View(shirt);
            }
        }
        catch (WebApiException ex)
        {
            HandleWebApiException(ex);
            return View(nameof(Index), await _webApiExecuter.InvokeGet<IEnumerable<Shirt>>("shirt") );
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(Shirt shirt)
    {
        if (!ModelState.IsValid)
        {
            return View(shirt);
        }
        try
        {
            await _webApiExecuter.InvokePut($"shirt/{shirt.Id}", shirt);
        }
        catch (WebApiException ex)
        {
            HandleWebApiException(ex);
            return View(shirt);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _webApiExecuter.InvokeDelete($"shirt/{id}");
        }
        catch (WebApiException ex)
        {
            HandleWebApiException(ex);
            return View(nameof(Index), await _webApiExecuter.InvokeGet<IEnumerable<Shirt>>("shirt") );
        }
        return RedirectToAction(nameof(Index));
    }

    private void HandleWebApiException(WebApiException ex)
    {
        if (ex.ErrorResponse is not null &&
            ex.ErrorResponse.Errors is not null &&
            ex.ErrorResponse.Errors.Count > 0)
        {
            foreach (var error in ex.ErrorResponse.Errors)
            {
                ModelState.AddModelError(error.Key, string.Join("; ",error.Value));
            }
        }
    }
}