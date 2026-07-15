using FluentValidation;

namespace ShelfLife.Catalog.Application.Books.Queries.GetSessionsForBook
{
    public class GetSessionsForBookQueryValidator : AbstractValidator<GetSessionsForBookQuery>
    {
        public GetSessionsForBookQueryValidator()
        {
            RuleFor(x => x.BookId).NotEqual(Guid.Empty).WithMessage("BookId cannot be empty.");
        }
    }
}
