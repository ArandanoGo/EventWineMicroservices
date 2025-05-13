using BURUBERI.InventoryService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BURUBERI.InventoryService.API.Data;

public class LotRepository : ILotRepository
{
    private readonly ApplicationDbContext _context;

    public LotRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Lot>> GetByProducerAsync(Guid producerId)
    {
        return await _context.Lots
            .Where(l => l.ProducerId == producerId)
            .ToListAsync();
    }

    public async Task<Lot?> GetAsync(Guid lotId)
    {
        return await _context.Lots.FindAsync(lotId);
    }

    public async Task AddAsync(Lot lot)
    {
        await _context.Lots.AddAsync(lot);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Lot lot)
    {
        lot.UpdatedAt = DateTime.UtcNow;
        _context.Lots.Update(lot);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Lot lot)
    {
        _context.Lots.Remove(lot);
        await _context.SaveChangesAsync();
    }

    // Implementación de ReserveStockAsync
    public async Task<bool> ReserveStockAsync(Guid lotId, double quantity)
    {
        var lot = await _context.Lots.FindAsync(lotId);
        if (lot == null || lot.Stock < quantity)
        {
            return false; // No hay suficiente stock o el lote no existe
        }

        lot.Stock -= quantity; // Se reserva la cantidad
        await _context.SaveChangesAsync();
        return true; // Reserva exitosa
    }

    // Implementación de ReleaseStockAsync
    public async Task<bool> ReleaseStockAsync(Guid lotId, double quantity)
    {
        var lot = await _context.Lots.FindAsync(lotId);
        if (lot == null)
        {
            return false; // El lote no existe
        }

        lot.Stock += quantity; // Se libera la cantidad reservada
        await _context.SaveChangesAsync();
        return true; // Liberación exitosa
    }
}