using MediatR;
using ShelfLife.Catalog.Application.Sessions.DTos;

namespace ShelfLife.Catalog.Application.Sessions.Queries
{
    public record GetSessionsForBookQuery(Guid BookId) : IRequest<IEnumerable<ReadingSessionDto>>;
}
