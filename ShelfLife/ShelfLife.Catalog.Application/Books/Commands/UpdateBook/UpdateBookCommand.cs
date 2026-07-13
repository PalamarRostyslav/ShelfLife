using MediatR;
using ShelfLife.Catalog.Domain.Entities;

namespace ShelfLife.Catalog.Application.Books.Commands.UpdateBook
{
    public record UpdateBookCommand(Guid BookId, string Title, string Author, string? ISBN, int PageCount, BookFormat Format, int? Rating) : IRequest;
}
