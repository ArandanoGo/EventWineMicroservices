using Microsoft.AspNetCore.Mvc;
using RegistryService.API.Models;
using RegistryService.API.Services;

namespace RegistryService.API.Controllers;

[ApiController]
[Route("registry")]
public class RegistryController : ControllerBase
{
    private readonly ServiceRegistry _registry;

    public RegistryController(ServiceRegistry registry)
    {
        _registry = registry;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] ServiceInfo service)
    {
        _registry.Register(service);
        return Ok($"Service '{service.Name}' registered at '{service.Url}'");
    }

    [HttpGet("services")]
    public IActionResult GetAll()
    {
        return Ok(_registry.GetAll());
    }

    [HttpGet("services/{name}")]
    public IActionResult GetByName(string name)
    {
        var service = _registry.Get(name);
        return service is not null ? Ok(service) : NotFound();
    }
}