using ShelfLife.Catalog.Domain.Entities;
namespace ShelfLife.Catalog.Application.Common.Interfaces
{
    public interface ICatalogDbContext
    {
        IQueryable<Book> Books { get; }

        IQueryable<Shelf> Shelves { get; }

        IQueryable<ReadingSession> Sessions { get; }
    }
}
