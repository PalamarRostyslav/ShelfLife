using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Specifications;

namespace ShelfLife.Catalog.Application.Common.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<List<Book>> ListAsync(ISpecification<Book> spec, CancellationToken ct = default);

        void Add(Book book);

        void Remove(Book book);
    }
}
