using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Specifications;

namespace ShelfLife.Catalog.Application.Common.Interfaces
{
    public interface IShelfRepository
    {
        Task<Shelf?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<List<Shelf>> ListAsync(ISpecification<Shelf> spec, CancellationToken ct = default);

        void Add(Shelf shelf);

        void Remove(Shelf shelf);
    }
}
