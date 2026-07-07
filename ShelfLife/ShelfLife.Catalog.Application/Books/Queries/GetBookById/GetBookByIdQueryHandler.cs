using MediatR;
using ShelfLife.Catalog.Application.Books.DTOs;
using ShelfLife.Catalog.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ShelfLife.Catalog.Application.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto?>
    {
        private readonly ICatalogDbContext _db;

        public GetBookByIdQueryHandler(ICatalogDbContext db) => _db = db;

        public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _db.Books
            .Where(b => b.Id == request.BookId)
            .Select(b => new BookDto(b.Id, b.Title!, b.Author!, b.ISBN, b.PageCount,
                b.Format.ToString(), b.Rating, b.ShelfId, b.AddedAt, b.FinishedAt))
            .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
