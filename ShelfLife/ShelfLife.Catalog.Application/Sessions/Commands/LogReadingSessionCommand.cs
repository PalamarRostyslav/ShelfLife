using MediatR;

namespace ShelfLife.Catalog.Application.Sessions.Commands
{
    public record LogReadingSessionCommand(Guid BookId, DateTime StartTime, int PagesRead, int MinutesSpent) : IRequest<Guid>;
}
