using FluentValidation;

namespace ShelfLife.Catalog.Application.Books.Commands.DeleteBook
{
    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            RuleFor(x => x.BookId).NotEqual(Guid.Empty).WithMessage("BookId cannot be empty.");
        }
    }
}
