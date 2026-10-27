using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FeriaEmprendimiento.Models;
using FeriaEmprendimiento.Services;

namespace FeriaEmprendimiento.Controllers;

public class HomeController : Controller
{
    private readonly IEmprendimientoService _servicio;

    public HomeController(IEmprendimientoService servicio)
    {
        _servicio = servicio;
    }

    public IActionResult Index()
    {
        var viewModel = _servicio.ObtenerCatalogoViewModel();
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
