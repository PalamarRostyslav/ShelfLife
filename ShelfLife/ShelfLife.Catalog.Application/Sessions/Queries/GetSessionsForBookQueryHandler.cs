using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Application.Sessions.DTos;

namespace ShelfLife.Catalog.Application.Sessions.Queries
{
    public class GetSessionsForBookQueryHandler : IRequestHandler<GetSessionsForBookQuery, IEnumerable<ReadingSessionDto>>
    {
        private readonly ICatalogDbContext _db;

        public GetSessionsForBookQueryHandler(ICatalogDbContext db) => _db = db;

        public async Task<IEnumerable<ReadingSessionDto>> Handle(GetSessionsForBookQuery request, CancellationToken cancellationToken)
        {
            return await _db.Sessions
                .Where(s => s.BookId == request.BookId)
                .OrderByDescending(s => s.Date)
                .Select(s => new ReadingSessionDto(s.Id, s.BookId, s.Date, s.PagesRead, s.MinutesSpent))
                .ToListAsync();
        }
    }
}
