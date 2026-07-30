namespace ShelfLife.Gateway.Clients
{
    public interface ICatalogServiceClient
    {
        Task<BookSummaryDto?> GetBookAsync(Guid bookId, CancellationToken cancellationToken = default);
    }
}
