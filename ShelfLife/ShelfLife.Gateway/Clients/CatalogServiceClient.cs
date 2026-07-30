using System.Net;

namespace ShelfLife.Gateway.Clients
{
    public class CatalogServiceClient : ICatalogServiceClient
    {
        private readonly HttpClient _http;

        public CatalogServiceClient(HttpClient http) => _http = http;

        public async Task<BookSummaryDto?> GetBookAsync(Guid bookId, CancellationToken cancellationToken = default)
        {
            var response = await _http.GetAsync($"/api/books/{bookId}", cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BookSummaryDto>(cancellationToken: cancellationToken);
        }
    }
}
