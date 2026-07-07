using Microsoft.EntityFrameworkCore;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Domain.Entities;

namespace ShelfLife.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext, IUnitOfWork, ICatalogDbContext
    {
        public DbSet<Book> Books => Set<Book>();

        public DbSet<Shelf> Shelves => Set<Shelf>();

        public DbSet<ReadingSession> Sessions => Set<ReadingSession>();

        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

        IQueryable<Book> ICatalogDbContext.Books => Books;

        IQueryable<Shelf> ICatalogDbContext.Shelves => Shelves;

        IQueryable<ReadingSession> ICatalogDbContext.Sessions => Sessions;

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }
    }
}
