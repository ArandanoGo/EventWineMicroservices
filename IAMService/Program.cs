using IAMService.IAM.Application.Internal.CommandServices;
using IAMService.IAM.Application.Internal.OutboundServices;
using IAMService.IAM.Application.Internal.QueryServices;
using IAMService.IAM.Domain.Repositories;
using IAMService.IAM.Domain.Services;
using IAMService.IAM.Infrastructure.Hashing.BCrypt.Services;
using IAMService.IAM.Infrastructure.Persistence.EFC.Repositories;
using IAMService.IAM.Infrastructure.Tokens.JWT.Configuration;
using IAMService.IAM.Infrastructure.Tokens.JWT.Services;
using IAMService.Profiles.Application.Internal.CommandServices;
using IAMService.Profiles.Application.Internal.QueryServices;
using IAMService.Profiles.Domain.Repositories;
using IAMService.Profiles.Domain.Services;
using IAMService.Profiles.Infrastructure.Persistence.EFC.Repositories;
using IAMService.Shared.Domain.Repositories;
using IAMService.Shared.Infrastructure.Interfaces.ASP.Configuration;
using IAMService.Shared.Infrastructure.Persistence.EFC.Configuration;
using IAMService.Shared.Infrastructure.Persistence.EFC.Repositories;
using IAMService.Shared.Infrastructure.Pipeline.Middleware.Components;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configura el servicio CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// =================================== Servicios Web API ===================================
builder.Services.AddControllers();
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
// ==========================================================================================

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new InvalidOperationException("Connection string not found.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
    }
    else if (builder.Environment.IsProduction())
    {
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Error);
    }
});

// ============================= Swagger / OpenAPI =============================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());
// ============================================================================

// ================================== Inyecciones =============================
builder.Services.AddScoped<IUnitOfWOrk, UnitOfWork>();

// -------- Profiles
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

// -------- IAM
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

// -------- Shared
builder.Services.AddExceptionHandler<CommonExceptionHandler>();
builder.Services.AddProblemDetails();
// ============================================================================

var app = builder.Build();

// Aplica la política CORS globalmente
app.UseCors("AllowAll");

// =================== Verifica y crea la base de datos si no existe ===================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}
// =====================================================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

// ============ REGISTRO EN RegistryService ============
try
{
    var serviceInfo = new
    {
        name = "iam-service",
        url = "http://localhost:5085"
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
// =====================================================

app.Run();
