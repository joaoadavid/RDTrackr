using Microsoft.EntityFrameworkCore;
using RDTrackR.Domain.Entities;

namespace RDTrackR.Infrastructure.DataAccess
{
    public class RDTrackRDbContext : DbContext
    {
        public RDTrackRDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<CodeToPerformAction> CodeToPerformActions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Movement> Movements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RDTrackRDbContext).Assembly);
        }
    }
}
