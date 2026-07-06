using MediatR;
using ShelfLife.Catalog.Application.Books.DTOs;

namespace ShelfLife.Catalog.Application.Books.Queries.GetBookById
{
    public record GetBookByIdQuery(Guid BookId) : IRequest<BookDto?>;
}
