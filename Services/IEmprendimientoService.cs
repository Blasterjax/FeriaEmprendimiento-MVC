using FeriaEmprendimiento.Models;

namespace FeriaEmprendimiento.Services;

public interface IEmprendimientoService
{
    IEnumerable<Emprendimiento> ObtenerTodos(string? categoria = null, string? busqueda = null);
    Emprendimiento? ObtenerPorId(int id);
    void Agregar(Emprendimiento emprendimiento);
    bool Actualizar(Emprendimiento emprendimiento);
    bool Eliminar(int id);
    List<string> ObtenerCategorias();
    CatalogoViewModel ObtenerCatalogoViewModel(string? categoria = null, string? busqueda = null);
}
