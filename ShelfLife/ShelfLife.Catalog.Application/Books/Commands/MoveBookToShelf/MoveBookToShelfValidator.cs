using FluentValidation;

namespace ShelfLife.Catalog.Application.Books.Commands.MoveBookToShelf
{
    public class MoveBookToShelfValidator : AbstractValidator<MoveBookToShelfCommand>
    {
        public MoveBookToShelfValidator()
        {
            RuleFor(x => x.BookId).NotEqual(Guid.Empty).WithMessage("BookId cannot be empty.");
            RuleFor(x => x.ShelfId).NotEqual(Guid.Empty).WithMessage("ShelfId cannot be empty.");
        }
    }
}
