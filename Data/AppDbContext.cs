using Microsoft.EntityFrameworkCore;
using StockControl.Models; // Projenin adını (MVC_Example veya StockControl) kontrol et ve buna göre değiştir

namespace StockControl.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<Part> Parts { get; set; }
    }
}
