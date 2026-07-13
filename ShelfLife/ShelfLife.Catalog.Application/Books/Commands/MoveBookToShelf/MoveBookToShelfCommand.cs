using MediatR;

namespace ShelfLife.Catalog.Application.Books.Commands.MoveBookToShelf
{
    public record MoveBookToShelfCommand(Guid BookId, Guid ShelfId) : IRequest;
}
