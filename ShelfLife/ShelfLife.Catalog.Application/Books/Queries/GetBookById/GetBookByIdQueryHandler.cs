using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfLife.Catalog.Application.Books.DTOs;
using ShelfLife.Catalog.Infrastructure.Persistence;

namespace ShelfLife.Catalog.Application.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto?>
    {
        private readonly CatalogDbContext _db;

        public GetBookByIdQueryHandler(CatalogDbContext db) => _db = db;

        public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _db.Books.Where(b => b.Id == request.BookId).Select(b => new BookDto(b.Id, b.Title!, b.Author!, b.ISBN, b.PageCount,
                b.Format.ToString(), b.Rating, b.ShelfId, b.AddedAt, b.FinishedAt)).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
