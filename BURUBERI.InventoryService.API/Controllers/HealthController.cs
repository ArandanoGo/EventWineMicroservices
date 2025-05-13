using Microsoft.AspNetCore.Mvc;

namespace BURUBERI.InventoryService.API.Controllers
{
    [ApiController]
    [Route("")]
    public class HealthController : ControllerBase
    {
        // Para AdminService (llama a /status)
        [HttpGet("status")]
        // Para Gateway (llama a /inventory/status)
        [HttpGet("inventory/status")]
        public IActionResult Status()
        {
            return Ok("Inventory Service is alive");
        }
    }
}