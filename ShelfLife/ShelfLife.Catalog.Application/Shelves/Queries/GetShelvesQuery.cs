using MediatR;
using ShelfLife.Catalog.Application.Shelves.DTOs;

namespace ShelfLife.Catalog.Application.Shelves.Queries
{
    public record GetShelvesQuery : IRequest<IEnumerable<ShelfDto>>;
}
