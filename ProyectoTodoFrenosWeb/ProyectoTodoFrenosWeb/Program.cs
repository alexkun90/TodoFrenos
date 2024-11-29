using DAL.Models;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ProyectoTodoFrenosWeb.ConsumoServices;
using ProyectoTodoFrenosWeb.ViewModels;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConexiBD") ?? throw new InvalidOperationException("Connection string 'ConexiBD' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuracion BD y el context
builder.Services.AddDbContext<TodoFrenosDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Configuracion Indentity y el context
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


// Configuración de cookies para cerrar sesión al cerrar el navegador
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true; // La cookie solo es accesible por HTTP
    options.Cookie.IsEssential = true; // Obligatoria para el cumplimiento de GDPR si aplica
    options.ExpireTimeSpan = TimeSpan.FromMinutes(180); // Tiempo máximo de sesión (opcional)
    options.SlidingExpiration = true; // Renueva la cookie si hay actividad
    options.Cookie.MaxAge = null; // Evita que sea persistente
    options.LoginPath = "/Account/Login"; // Ruta de inicio de sesión
    options.LogoutPath = "/Account/Logout"; // Ruta de cierre de sesión
    options.AccessDeniedPath = "/Account/AccessDenied"; // Ruta de acceso denegado
});



builder.Services.AddHttpClient();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Configura los requisitos de la contraseÃ±a
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false; // O true si quieres incluir caracteres especiales
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

#region DI
    builder.Services.AddScoped<HttpClientService>();
#endregion

//Configuracion para las paginas o vistas
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Inicio/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Inicio}/{id?}");

app.MapRazorPages();

app.Run();
