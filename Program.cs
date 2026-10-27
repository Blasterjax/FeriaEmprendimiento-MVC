using FeriaEmprendimiento.Services;

var builder = WebApplication.CreateBuilder(args);

// Registro de servicios en el contenedor de Inyección de Dependencias (DI)
builder.Services.AddControllersWithViews();

// Registramos el servicio de Emprendimientos como Singleton para persistir los datos en memoria
builder.Services.AddSingleton<IEmprendimientoService, EmprendimientoService>();

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP (Middlewares)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
