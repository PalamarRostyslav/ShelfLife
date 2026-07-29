using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Application.Shelves.DTOs;

namespace ShelfLife.Catalog.Application.Shelves.Queries
{
    public class GetShelvesQueryHandler : IRequestHandler<GetShelvesQuery, IEnumerable<ShelfDto>>
    {
        private readonly ICatalogDbContext _db;

        public GetShelvesQueryHandler(ICatalogDbContext db) => _db = db;

        public async Task<IEnumerable<ShelfDto>> Handle(GetShelvesQuery request, CancellationToken cancellationToken)
        {
            return await _db.Shelves
                .OrderBy(s => s.IsSystemShelf ? 0 : 1).ThenBy(s => s.Name)
                .Select(s => new ShelfDto(s.Id, s.Name!, s.IsSystemShelf, s.SystemShelfType!.ToString()))
                .ToListAsync(cancellationToken);
        }
    }
}
