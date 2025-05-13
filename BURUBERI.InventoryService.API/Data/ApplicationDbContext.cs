using Microsoft.EntityFrameworkCore;
using BURUBERI.InventoryService.API.Models;  // Importación de modelos

namespace BURUBERI.InventoryService.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts)
            : base(opts) { }

        // Solo se necesita el DbSet de Lot ahora
        public DbSet<Lot> Lots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // No es necesario configurar nada más si ProducerId no tiene relación con otra entidad
            // No hay necesidad de configurar claves foráneas adicionales si no se usan relaciones entre tablas
        }
    }
}