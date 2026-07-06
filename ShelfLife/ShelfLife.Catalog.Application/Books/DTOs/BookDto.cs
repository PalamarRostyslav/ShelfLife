namespace ShelfLife.Catalog.Application.Books.DTOs
{
    public record BookDto(Guid Id, string Title, string Author, string? Isbn, int PageCount, string Format, int? Rating, Guid ShelfId, DateTime AddedAt, DateTime? FinishedAt);
}
