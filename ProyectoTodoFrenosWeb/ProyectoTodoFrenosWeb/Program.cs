using DAL.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuracion BD y el context

var connectionString = builder.Configuration.GetConnectionString("ConexiBD") ?? throw new InvalidOperationException("Connection string 'ConexiBD' not found.");

builder.Services.AddDbContext<TodoFrenosDbContext>(options => options.UseSqlServer(connectionString));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Inicio}/{id?}");

app.Run();
