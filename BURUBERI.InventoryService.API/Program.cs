using BURUBERI.InventoryService.API;
using BURUBERI.InventoryService.API.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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
    context.Database.EnsureCreated();  // Esto crea la base de datos si no existe
}

app.Run();