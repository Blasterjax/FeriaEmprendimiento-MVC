using System.ComponentModel.DataAnnotations;

namespace FeriaEmprendimiento.Models;

public class Emprendimiento
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [Display(Name = "Nombre del Emprendimiento")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La categoría es obligatoria")]
    [Display(Name = "Categoría")]
    public string Categoria { get; set; } = string.Empty;

    [Required(ErrorMessage = "El número de stand es obligatorio")]
    [Display(Name = "Número de Stand")]
    public string NumeroStand { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0, 100000000, ErrorMessage = "El precio debe ser un valor válido")]
    [Display(Name = "Precio (COP)")]
    public decimal PrecioBase { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria")]
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; } = string.Empty;

    [Display(Name = "Fundador / Contacto")]
    public string NombreEmprendedor { get; set; } = string.Empty;

    [Display(Name = "Correo de Contacto")]
    public string CorreoContacto { get; set; } = string.Empty;

    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = string.Empty;

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public bool Activo { get; set; } = true;
}
