namespace ShelfLife.Catalog.Application.Sessions.DTos
{
    public record ReadingSessionDto(Guid Id, Guid BookId, DateTime StartTime, int PagesRead, int MinutesSpent);
}
