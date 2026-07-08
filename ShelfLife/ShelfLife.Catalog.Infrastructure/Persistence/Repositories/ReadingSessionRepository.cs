using Microsoft.EntityFrameworkCore;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Specifications;

namespace ShelfLife.Catalog.Infrastructure.Persistence.Repositories
{
    public class ReadingSessionRepository : IReadingSessionRepository
    {
        private readonly CatalogDbContext _db;

        public ReadingSessionRepository(CatalogDbContext db) => _db = db;

        public Task<ReadingSession?> GetByIdAsync(Guid id, CancellationToken ct = default) => _db.Sessions.FirstOrDefaultAsync(s => s.Id == id, ct);

        public Task<List<ReadingSession>> ListAsync(ISpecification<ReadingSession> spec, CancellationToken ct = default) => SpecificationEvaluator<ReadingSession>.GetQuery(_db.Sessions, spec).ToListAsync(ct);

        public void Add(ReadingSession session) => _db.Sessions.Add(session);
    }
}
