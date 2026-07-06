using MediatR;
using ShelfLife.Catalog.Domain.Entities;

namespace ShelfLife.Catalog.Application.Books.Commands.AddBook
{
    public record AddBookCommand(string Title, string Author, string ISBN, int PageCount, BookFormat Format, Guid ShelfId) : IRequest<Guid>;
}
