using MediatR;

namespace ShelfLife.Catalog.Application.Shelves.Commands
{
    public record CreateShelfCommand(string Name) : IRequest<Guid>;
}
