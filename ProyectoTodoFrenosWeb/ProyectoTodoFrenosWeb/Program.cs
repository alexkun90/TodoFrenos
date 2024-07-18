using OpenAI_API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConexiBD") ?? throw new InvalidOperationException("Connection string 'ConexiBD' not found.");
//builder.Services.AddSingleton(new OpenAIAPI(new APIAuthentication()));
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<OpenAI_API>


//Configuracion BD y el context

builder.Services.AddDbContext<TodoFrenosDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//Configuracion Indentity y el context
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AuthDbContext>().AddDefaultUI();

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

app.MapControllerRoute(
    name: "appointments",
    pattern: "Appointments/{action}/{id?}",
    defaults: new { controller = "Appointments" });

app.MapRazorPages();

app.Run();
