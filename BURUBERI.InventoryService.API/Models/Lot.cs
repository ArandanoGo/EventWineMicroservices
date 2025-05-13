namespace BURUBERI.InventoryService.API.Models;

public class Lot
{
    public Guid Id { get; set; }                    // ID único del lote
    public Guid ProducerId { get; set; }            // ID del productor

    public string Type { get; set; }                // Tipo de arándano (ej: "Biloxi", "Emerald", etc.)
    public double WeightKg { get; set; }            // Peso total del lote en kilogramos
    public decimal UnitPrice { get; set; }          // Precio por kilogramo
    public string Quality { get; set; }             // Calidad (ej: "A1", "B", "Premium", etc.)
    public string Status { get; set; }              // Estado del lote ("Disponible", "Reservado", "Vendido", etc.)
    public double Stock { get; set; }               // Stock actual disponible (en kg)

    public string LotNumber { get; set; }           // Código o número de lote
    public DateTime ProducedAt { get; set; }        // Fecha de producción
    public DateTime ExpiresAt { get; set; }         // Fecha de expiración

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}