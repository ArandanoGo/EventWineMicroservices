using Microsoft.AspNetCore.Mvc;
using AdminService.API.Services;

namespace AdminService.API.Controllers;

[ApiController]
[Route("admin")]
public class AdminController : ControllerBase
{
    private readonly ServiceMonitor _monitor;

    public AdminController(ServiceMonitor monitor)
    {
        _monitor = monitor;
    }

    [HttpGet("status")]
    public async Task<IActionResult> CheckServices()
    {
        var result = await _monitor.CheckAllAsync();
        return Ok(result);
    }
}