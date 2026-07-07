using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Specifications;

namespace ShelfLife.Catalog.Application.Books.Specifications
{
    public class BooksByShelfSpec : Specification<Book>
    {
        public BooksByShelfSpec(Guid shelfId) => ApplyCriteria(b => b.ShelfId == shelfId);
    }
}
