using MediatR;

namespace ShelfLife.Catalog.Application.Books.Commands.DeleteBook
{
    public record DeleteBookCommand(Guid BookId) : IRequest;
}
