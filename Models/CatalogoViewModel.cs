namespace FeriaEmprendimiento.Models;

public class CatalogoViewModel
{
    public IEnumerable<Emprendimiento> Emprendimientos { get; set; } = new List<Emprendimiento>();
    public string? CategoriaSeleccionada { get; set; }
    public string? TerminoBusqueda { get; set; }
    public int TotalStands { get; set; }
    public int TotalCategorias { get; set; }
    public decimal PromedioPrecios { get; set; }
    public List<string> CategoriasDisponibles { get; set; } = new();
}
