using BURUBERI.InventoryService.API.Data;
using BURUBERI.InventoryService.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BURUBERI.InventoryService.API.Controllers;

[ApiController]
[Route("api/producers/{producerId:guid}/[controller]")]
public class LotController : ControllerBase
{
    private readonly ILotRepository _repo;

    public LotController(ILotRepository repo) => _repo = repo;

    // GET /api/producers/{producerId}/lots
    [HttpGet]
    public async Task<IActionResult> GetByProducer(Guid producerId)
        => Ok(await _repo.GetByProducerAsync(producerId));

    // POST /api/producers/{producerId}/lots
    [HttpPost]
    public async Task<IActionResult> Create(Guid producerId, Lot lot)
    {
        lot.Id = Guid.NewGuid();
        lot.ProducerId = producerId;  // Cambié ProductId por ProducerId
        await _repo.AddAsync(lot);
        return CreatedAtAction(nameof(GetByProducer), new { producerId }, lot);
    }

    // PUT /api/producers/{producerId}/lots/{lotId}/reserve?quantity=5
    [HttpPut("{lotId:guid}/reserve")]
    public async Task<IActionResult> Reserve(Guid producerId, Guid lotId, [FromQuery] double quantity)
    {
        var lot = await _repo.GetAsync(lotId);
        if (lot == null || lot.ProducerId != producerId) return NotFound();  // Cambié ProductId por ProducerId
        if (lot.Stock < quantity) return BadRequest("Stock insuficiente.");
        
        var success = await _repo.ReserveStockAsync(lotId, quantity);
        if (!success) return BadRequest("No se pudo realizar la reserva.");

        return NoContent();
    }

    // PUT /api/producers/{producerId}/lots/{lotId}/release?quantity=5
    [HttpPut("{lotId:guid}/release")]
    public async Task<IActionResult> Release(Guid producerId, Guid lotId, [FromQuery] double quantity)
    {
        var lot = await _repo.GetAsync(lotId);
        if (lot == null || lot.ProducerId != producerId) return NotFound();  // Cambié ProductId por ProducerId
        if (quantity <= 0) return BadRequest("La cantidad a liberar debe ser positiva.");
        
        var success = await _repo.ReleaseStockAsync(lotId, quantity);
        if (!success) return BadRequest("No se pudo liberar el stock.");

        return NoContent();
    }

    // PUT /api/producers/{producerId}/lots/{lotId}/confirm?quantity=5
    [HttpPut("{lotId:guid}/confirm")]
    public async Task<IActionResult> Confirm(Guid producerId, Guid lotId, [FromQuery] double quantity)
    {
        var lot = await _repo.GetAsync(lotId);
        if (lot == null || lot.ProducerId != producerId) return NotFound();  // Cambié ProductId por ProducerId
        if (lot.Stock < quantity) return BadRequest("Stock insuficiente.");
        
        // Lógica para confirmar la reserva (por ejemplo, disminuir el stock final)
        lot.Stock -= quantity;
        await _repo.UpdateAsync(lot);

        return NoContent();
    }
    [HttpGet("status")]
    public IActionResult Status()
    {
        return Ok("Inventory Service is alive");
    }

}
