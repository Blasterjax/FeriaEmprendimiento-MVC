# Feria Universitaria de Emprendimiento

**Estudiante:** Samuel Cortés Guirales  
**Asignatura:** Programación Web  
**Temática:** Feria de Emprendimiento  

---

## Descripción del Proyecto

Aplicación web desarrollada con ASP.NET Core MVC y C# para la gestión y consulta de stands en la Feria de Emprendimiento, dando continuidad a los Seguimientos 01 y 02.

El proyecto implementa el patrón Modelo-Vista-Controlador (MVC), separación de responsabilidades, validaciones en el servidor con Data Annotations, vistas Razor con Bootstrap y persistencia de datos en memoria mediante un servicio registrado en el contenedor de dependencias.

---

## Estructura del Proyecto

* **Models:** Entidad `Emprendimiento.cs` con propiedades y validaciones, y `CatalogoViewModel.cs` para el envío de datos a la vista.
* **Controllers:** `EmprendimientoController.cs` con acciones GET y POST para catálogo, registro, edición, detalle y eliminación de stands, y `HomeController.cs` para la página de inicio.
* **Views:** Vistas Razor (.cshtml) organizadas por controlador, utilizando HTML, Bootstrap y Tag Helpers.
* **Services:** `EmprendimientoService.cs` para la persistencia y manipulación de datos en memoria.
* **wwwroot:** Archivos estáticos como CSS, JavaScript y librerías de Bootstrap.
