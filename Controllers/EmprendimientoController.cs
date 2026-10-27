using Microsoft.AspNetCore.Mvc;
using FeriaEmprendimiento.Models;
using FeriaEmprendimiento.Services;

namespace FeriaEmprendimiento.Controllers;

public class EmprendimientoController : Controller
{
    private readonly IEmprendimientoService _servicio;

    public EmprendimientoController(IEmprendimientoService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public IActionResult Index(string? categoria, string? busqueda)
    {
        var viewModel = _servicio.ObtenerCatalogoViewModel(categoria, busqueda);
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Detalles(int id)
    {
        var stand = _servicio.ObtenerPorId(id);
        if (stand == null)
        {
            TempData["Mensaje"] = "El stand no fue encontrado.";
            TempData["TipoMensaje"] = "warning";
            return RedirectToAction(nameof(Index));
        }

        return View(stand);
    }

    [HttpGet]
    public IActionResult Crear()
    {
        ViewBag.Categorias = _servicio.ObtenerCategorias();
        return View(new Emprendimiento());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Emprendimiento modelo)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categorias = _servicio.ObtenerCategorias();
            TempData["Mensaje"] = "Por favor completa todos los campos requeridos.";
            TempData["TipoMensaje"] = "danger";
            return View(modelo);
        }

        _servicio.Agregar(modelo);

        TempData["Mensaje"] = $"El emprendimiento '{modelo.Nombre}' fue registrado correctamente en el stand {modelo.NumeroStand}.";
        TempData["TipoMensaje"] = "success";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        var stand = _servicio.ObtenerPorId(id);
        if (stand == null)
        {
            TempData["Mensaje"] = "El stand solicitado no existe.";
            TempData["TipoMensaje"] = "warning";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Categorias = _servicio.ObtenerCategorias();
        return View(stand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Emprendimiento modelo)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categorias = _servicio.ObtenerCategorias();
            return View(modelo);
        }

        bool actualizado = _servicio.Actualizar(modelo);
        if (!actualizado)
        {
            TempData["Mensaje"] = "No se pudo actualizar el registro.";
            TempData["TipoMensaje"] = "danger";
            return RedirectToAction(nameof(Index));
        }

        TempData["Mensaje"] = $"El stand '{modelo.Nombre}' fue actualizado.";
        TempData["TipoMensaje"] = "success";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id)
    {
        var stand = _servicio.ObtenerPorId(id);
        string nombre = stand?.Nombre ?? "Stand";

        bool eliminado = _servicio.Eliminar(id);
        if (eliminado)
        {
            TempData["Mensaje"] = $"El stand '{nombre}' fue eliminado del catálogo.";
            TempData["TipoMensaje"] = "info";
        }

        return RedirectToAction(nameof(Index));
    }
}
