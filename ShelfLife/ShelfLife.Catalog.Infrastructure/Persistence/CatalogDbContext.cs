using Microsoft.EntityFrameworkCore;
using ShelfLife.Catalog.Domain.Entities;

namespace ShelfLife.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();

        public DbSet<Shelf> Shelves => Set<Shelf>();

        public DbSet<ReadingSession> Sessions => Set<ReadingSession>();

        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }
    }
}
