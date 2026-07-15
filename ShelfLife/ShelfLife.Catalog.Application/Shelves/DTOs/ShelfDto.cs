namespace ShelfLife.Catalog.Application.Shelves.DTOs
{
    public record ShelfDto(Guid Id, string Name, bool IsSystemShelf, string? SystemShelfType);
}
