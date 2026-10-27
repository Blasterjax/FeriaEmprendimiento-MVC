using FeriaEmprendimiento.Models;

namespace FeriaEmprendimiento.Services;

public class EmprendimientoService : IEmprendimientoService
{
    private readonly List<Emprendimiento> _emprendimientos = new();
    private readonly object _lock = new();
    private int _siguienteId = 1;

    public EmprendimientoService()
    {
        InicializarDatos();
    }

    private void InicializarDatos()
    {
        var datosIniciales = new List<Emprendimiento>
        {
            new Emprendimiento
            {
                Id = _siguienteId++,
                Nombre = "EcoTech Soluciones",
                Categoria = "Tecnología",
                NumeroStand = "A-01",
                PrecioBase = 50000,
                Descripcion = "Dispositivos para el ahorro de energía en el hogar.",
                NombreEmprendedor = "Laura Gómez",
                CorreoContacto = "contacto@ecotech.com",
                Telefono = "3104567890",
                FechaRegistro = DateTime.Now.AddDays(-10),
                Activo = true
            },
            new Emprendimiento
            {
                Id = _siguienteId++,
                Nombre = "Café de la Sierra",
                Categoria = "Gastronomía",
                NumeroStand = "B-03",
                PrecioBase = 15000,
                Descripcion = "Café artesanal 100% orgánico y cultivado localmente.",
                NombreEmprendedor = "Carlos Pérez",
                CorreoContacto = "ventas@cafesierra.co",
                Telefono = "3209876543",
                FechaRegistro = DateTime.Now.AddDays(-8),
                Activo = true
            },
            new Emprendimiento
            {
                Id = _siguienteId++,
                Nombre = "Moda Verde",
                Categoria = "Moda",
                NumeroStand = "C-05",
                PrecioBase = 35000,
                Descripcion = "Prendas elaboradas con materiales textiles reciclados.",
                NombreEmprendedor = "Valentina Ruiz",
                CorreoContacto = "info@modaverde.org",
                Telefono = "3151234567",
                FechaRegistro = DateTime.Now.AddDays(-5),
                Activo = true
            },
            new Emprendimiento
            {
                Id = _siguienteId++,
                Nombre = "BioAbonos del Campo",
                Categoria = "Sostenibilidad",
                NumeroStand = "D-02",
                PrecioBase = 12000,
                Descripcion = "Fertilizantes orgánicos para huertas caseras y cultivos.",
                NombreEmprendedor = "Andrés Morales",
                CorreoContacto = "abonos@campoverde.com",
                Telefono = "3187654321",
                FechaRegistro = DateTime.Now.AddDays(-3),
                Activo = true
            }
        };

        _emprendimientos.AddRange(datosIniciales);
    }

    public IEnumerable<Emprendimiento> ObtenerTodos(string? categoria = null, string? busqueda = null)
    {
        lock (_lock)
        {
            var consulta = _emprendimientos.Where(e => e.Activo).AsQueryable();

            if (!string.IsNullOrWhiteSpace(categoria) && !categoria.Equals("Todos", StringComparison.OrdinalIgnoreCase))
            {
                consulta = consulta.Where(e => e.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                consulta = consulta.Where(e =>
                    e.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                    e.Descripcion.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                    e.NumeroStand.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                    e.NombreEmprendedor.Contains(busqueda, StringComparison.OrdinalIgnoreCase));
            }

            return consulta.OrderByDescending(e => e.Id).ToList();
        }
    }

    public Emprendimiento? ObtenerPorId(int id)
    {
        lock (_lock)
        {
            return _emprendimientos.FirstOrDefault(e => e.Id == id && e.Activo);
        }
    }

    public void Agregar(Emprendimiento emprendimiento)
    {
        lock (_lock)
        {
            emprendimiento.Id = _siguienteId++;
            emprendimiento.FechaRegistro = DateTime.Now;
            emprendimiento.Activo = true;
            _emprendimientos.Add(emprendimiento);
        }
    }

    public bool Actualizar(Emprendimiento emprendimiento)
    {
        lock (_lock)
        {
            var existente = _emprendimientos.FirstOrDefault(e => e.Id == emprendimiento.Id && e.Activo);
            if (existente == null) return false;

            existente.Nombre = emprendimiento.Nombre;
            existente.Categoria = emprendimiento.Categoria;
            existente.NumeroStand = emprendimiento.NumeroStand;
            existente.PrecioBase = emprendimiento.PrecioBase;
            existente.Descripcion = emprendimiento.Descripcion;
            existente.NombreEmprendedor = emprendimiento.NombreEmprendedor;
            existente.CorreoContacto = emprendimiento.CorreoContacto;
            existente.Telefono = emprendimiento.Telefono;

            return true;
        }
    }

    public bool Eliminar(int id)
    {
        lock (_lock)
        {
            var existente = _emprendimientos.FirstOrDefault(e => e.Id == id && e.Activo);
            if (existente == null) return false;

            existente.Activo = false;
            return true;
        }
    }

    public List<string> ObtenerCategorias()
    {
        return new List<string>
        {
            "Tecnología",
            "Gastronomía",
            "Moda",
            "Sostenibilidad"
        };
    }

    public CatalogoViewModel ObtenerCatalogoViewModel(string? categoria = null, string? busqueda = null)
    {
        var lista = ObtenerTodos(categoria, busqueda).ToList();
        var activos = _emprendimientos.Where(e => e.Activo).ToList();

        return new CatalogoViewModel
        {
            Emprendimientos = lista,
            CategoriaSeleccionada = categoria ?? "Todos",
            TerminoBusqueda = busqueda,
            TotalStands = activos.Count,
            TotalCategorias = activos.Select(e => e.Categoria).Distinct().Count(),
            PromedioPrecios = activos.Count > 0 ? activos.Average(e => e.PrecioBase) : 0,
            CategoriasDisponibles = ObtenerCategorias()
        };
    }
}
