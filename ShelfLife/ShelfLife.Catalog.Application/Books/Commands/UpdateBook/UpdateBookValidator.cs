using FluentValidation;

namespace ShelfLife.Catalog.Application.Books.Commands.UpdateBook
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(x=> x.BookId).NotEqual(Guid.Empty).WithMessage("BookId cannot be empty.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.").MaximumLength(500);
            RuleFor(x => x.Author).NotEmpty().WithMessage("Author cannot be empty.").MaximumLength(300);
            RuleFor(x => x.PageCount).GreaterThan(0).WithMessage("PageCount must be greater than 0.");
            RuleFor(x => x.Rating).InclusiveBetween(1, 5).When(x => x.Rating.HasValue).WithMessage("Rating must be between 1 and 5.");
        }
    }
}
