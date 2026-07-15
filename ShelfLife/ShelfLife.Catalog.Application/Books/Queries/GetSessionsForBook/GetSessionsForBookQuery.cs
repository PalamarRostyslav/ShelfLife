using MediatR;
using ShelfLife.Catalog.Application.Sessions.DTos;

namespace ShelfLife.Catalog.Application.Books.Queries.GetSessionsForBook
{
    public record GetSessionsForBookQuery(Guid BookId) : IRequest<List<ReadingSessionDto>>;
}
