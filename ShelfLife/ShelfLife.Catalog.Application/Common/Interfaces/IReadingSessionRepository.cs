using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Specifications;

namespace ShelfLife.Catalog.Application.Common.Interfaces
{
    public interface IReadingSessionRepository
    {
        Task<ReadingSession?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<List<ReadingSession>> ListAsync(ISpecification<ReadingSession> spec, CancellationToken ct = default);

        void Add(ReadingSession session);
    }
}
