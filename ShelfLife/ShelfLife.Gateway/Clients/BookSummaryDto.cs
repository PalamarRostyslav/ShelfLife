namespace ShelfLife.Gateway.Clients
{
    public record BookSummaryDto(Guid Id, string Title, string Author, Guid ShelfId);
}
