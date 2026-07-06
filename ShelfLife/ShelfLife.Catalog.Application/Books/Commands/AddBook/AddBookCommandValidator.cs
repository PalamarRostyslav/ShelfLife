using FluentValidation;

namespace ShelfLife.Catalog.Application.Books.Commands.AddBook
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Author).NotEmpty().MaximumLength(300);
            RuleFor(x => x.PageCount).GreaterThan(0);
            RuleFor(x => x.ShelfId).NotEqual(Guid.Empty);
        }
    }
}
