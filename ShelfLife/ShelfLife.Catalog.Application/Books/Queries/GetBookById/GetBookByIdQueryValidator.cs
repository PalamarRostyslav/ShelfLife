using FluentValidation;

namespace ShelfLife.Catalog.Application.Books.Queries.GetBookById
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(x => x.BookId).NotEqual(Guid.Empty);
        }
    }
}