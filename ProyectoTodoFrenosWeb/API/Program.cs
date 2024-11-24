using API.Middlewares;
using API.Services;
using DAL.Models;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexion con los modelos
builder.Services.AddDbContext<TodoFrenosDbContext>(options => options.UseSqlServer("name=ConexiBD"));
//
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

#region HangFire

builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("ConexiBD"), new SqlServerStorageOptions
          {
              CommandBatchMaxTimeout = TimeSpan.FromMinutes(2),
              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(2),
              QueuePollInterval = TimeSpan.FromSeconds(5),
              UseRecommendedIsolationLevel = true,
              DisableGlobalLocks = true
          }));

builder.Services.AddHangfireServer();
#endregion

builder.Services.AddScoped<OrderService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiKeyConfig>();

app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireDashboard();

//Tarea Automatica para la fecha de ordenes
using (var scope = app.Services.CreateScope())
{
    var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();

    RecurringJob.AddOrUpdate(
        "revisar-ordenes-retrasadas", // Nombre del trabajo
        () => orderService.ReviewAllOrders(), 
        Cron.Daily // Cronograma: Diariamente
    );
}

app.MapControllers();

app.Run();
