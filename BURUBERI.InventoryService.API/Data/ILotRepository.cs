using BURUBERI.InventoryService.API.Models;

namespace BURUBERI.InventoryService.API.Data;

public interface ILotRepository
{
    Task<IEnumerable<Lot>> GetByProducerAsync(Guid producerId);  // Obtener lotes por productor
    Task<Lot?> GetAsync(Guid lotId);                             // Obtener un lote específico
    Task AddAsync(Lot lot);                                       // Agregar un nuevo lote
    Task UpdateAsync(Lot lot);                                    // Actualizar un lote
    Task DeleteAsync(Lot lot);                                    // Eliminar un lote

    // Métodos adicionales para la lógica de reserva y liberación
    Task<bool> ReserveStockAsync(Guid lotId, double quantity);   // Reservar stock de un lote
    Task<bool> ReleaseStockAsync(Guid lotId, double quantity);   // Liberar stock de un lote
}