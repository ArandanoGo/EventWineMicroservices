using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

// Carga la configuración desde appsettings
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy(); // ← enruta todas las peticiones según config

app.Run();