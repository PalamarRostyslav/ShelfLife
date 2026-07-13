using FluentValidation;

namespace ShelfLife.Catalog.Application.Books.Queries.GetBooks
{
    public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator() 
        {
            RuleFor(x => x.ShelfId).NotEqual(Guid.Empty).WithMessage("ShelfId cannot be empty.");
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");
            RuleFor(x => x.Take).GreaterThan(0).WithMessage("Take must be greater than 0.");

        }
    }
}
