using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ConfigService.API.Controllers;

[ApiController]
[Route("config")]
public class ConfigController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public ConfigController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet("{serviceName}")]
    public IActionResult GetConfig(string serviceName)
    {
        var fileName = $"{serviceName}.json";
        var path = Path.Combine(_env.ContentRootPath,  "..", "config-data", fileName);

        Console.WriteLine("📂 Ruta buscada: " + Path.GetFullPath(path)); // debug

        if (!System.IO.File.Exists(path))
            return NotFound($"No config found for: {serviceName}");

        try
        {
            var json = System.IO.File.ReadAllText(path);
            var doc = JsonDocument.Parse(json);
            return Ok(doc.RootElement);
        }
        catch (JsonException ex)
        {
            return BadRequest($"Invalid JSON in config file: {ex.Message}");
        }
    }
}