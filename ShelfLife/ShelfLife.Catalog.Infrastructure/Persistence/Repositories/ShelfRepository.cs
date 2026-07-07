using Microsoft.EntityFrameworkCore;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Specifications;

namespace ShelfLife.Catalog.Infrastructure.Persistence.Repositories
{
    internal class ShelfRepository : IShelfRepository
    {
        private readonly CatalogDbContext _db;

        public ShelfRepository(CatalogDbContext db) => _db = db;

        public Task<Shelf?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => _db.Shelves.FirstOrDefaultAsync(s => s.Id == id, ct);

        public Task<List<Shelf>> ListAsync(ISpecification<Shelf> spec, CancellationToken ct = default)
            => SpecificationEvaluator<Shelf>.GetQuery(_db.Shelves, spec).ToListAsync(ct);

        public void Add(Shelf shelf) => _db.Shelves.Add(shelf);

        public void Remove(Shelf shelf) => _db.Shelves.Remove(shelf);
    }
}
