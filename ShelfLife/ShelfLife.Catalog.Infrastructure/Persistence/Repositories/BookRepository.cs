using Microsoft.EntityFrameworkCore;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Specifications;

namespace ShelfLife.Catalog.Infrastructure.Persistence.Repositories
{
    internal class BookRepository : IBookRepository
    {
        private readonly CatalogDbContext _db;

        public BookRepository(CatalogDbContext db) => _db = db;

        public Task<Book?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => _db.Books.FirstOrDefaultAsync(b => b.Id == id, ct);

        public Task<List<Book>> ListAsync(ISpecification<Book> spec, CancellationToken ct = default)
            => SpecificationEvaluator<Book>.GetQuery(_db.Books, spec).ToListAsync(ct);

        public void Add(Book book) => _db.Books.Add(book);

        public void Remove(Book book) => _db.Books.Remove(book);
    }
}
