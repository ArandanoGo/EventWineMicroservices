using Microsoft.AspNetCore.Mvc;

namespace IAMService.IAM.Interfaces.REST;

[ApiController]
[Route("iam")]
public class HealthController : ControllerBase
{
    [HttpGet("status")]
    [HttpGet("/status")]
    public IActionResult Status()
    {
        return Ok("IAM Service is alive");
    }
}