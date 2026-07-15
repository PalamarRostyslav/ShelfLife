using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Application.Sessions.DTos;

namespace ShelfLife.Catalog.Application.Books.Queries.GetSessionsForBook
{
    public class GetSessionsForBookQueryHandler : IRequestHandler<GetSessionsForBookQuery, List<ReadingSessionDto>>
    {
        private readonly ICatalogDbContext _db;

        public GetSessionsForBookQueryHandler(ICatalogDbContext db) => _db = db;

        public async Task<List<ReadingSessionDto>> Handle(GetSessionsForBookQuery request, CancellationToken cancellationToken)
        {
            return await _db.Sessions
                .Where(rs => rs.BookId == request.BookId)
                .OrderByDescending(rs => rs.Date)
                .Select(rs => new ReadingSessionDto(rs.Id, rs.BookId, rs.Date, rs.PagesRead, rs.MinutesSpent))
                .ToListAsync(cancellationToken);
        }
    }
}
