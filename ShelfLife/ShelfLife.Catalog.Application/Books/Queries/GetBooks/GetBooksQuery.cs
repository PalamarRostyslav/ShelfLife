using MediatR;
using ShelfLife.Catalog.Application.Books.DTOs;

namespace ShelfLife.Catalog.Application.Books.Queries.GetBooks
{
    public record GetBooksQuery(Guid? ShelfId, int Skip = 0, int Take = 50) : IRequest<List<BookDto>>;
}
