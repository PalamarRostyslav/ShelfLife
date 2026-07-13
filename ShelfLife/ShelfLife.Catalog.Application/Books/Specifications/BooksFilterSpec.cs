using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Specifications;

namespace ShelfLife.Catalog.Application.Books.Specifications
{
    public class BooksFilterSpec : Specification<Book>
    {
        public BooksFilterSpec(Guid? shelfId, int skip, int take)
        {
            if (shelfId.HasValue)
            {
                ApplyCriteria(b => b.ShelfId == shelfId.Value);
            }

            ApplyOrderByDescending(b => b.AddedAt);
            ApplyPaging(skip, take);
            // TODO (step 5): AND-combine with genre/Theme filter once Theme is modeled
        }
    }
}
