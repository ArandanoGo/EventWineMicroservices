using BURUBERI.InventoryService.API;
using BURUBERI.InventoryService.API.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ✅ Agrega servicios para Web API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Agrega tu DbContext (con cadena de conexión desde appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// ✅ Agrega tus repositorios
builder.Services.AddScoped<ILotRepository, LotRepository>();

// ✅ Agrega el servicio en segundo plano (Worker)
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// ✅ Middleware para Swagger y API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers(); // 👈 esto es CLAVE para activar tus controladores

// Llama a EnsureCreated() para crear la base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

// ✅ Registro en RegistryService.API (puerto 5002)
try
{
    var serviceInfo = new
    {
        name = "inventory-service",
        url = "http://localhost:5000" // ⚠️ usa el puerto real donde corre InventoryService
    };

    var json = JsonSerializer.Serialize(serviceInfo);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    using var client = new HttpClient();
    var response = await client.PostAsync("http://localhost:5002/registry/register", content);

    Console.WriteLine($"✅ Registro en RegistryService: {response.StatusCode}");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error registrando el servicio: {ex.Message}");
}

app.Run();
