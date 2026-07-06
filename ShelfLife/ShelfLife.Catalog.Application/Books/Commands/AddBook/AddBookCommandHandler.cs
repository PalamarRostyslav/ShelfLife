using MediatR;
using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.Catalog.Infrastructure.Persistence;

namespace ShelfLife.Catalog.Application.Books.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Guid>
    {
        private readonly CatalogDbContext _db;

        public AddBookCommandHandler(CatalogDbContext db)
        {
            _db = db;
        }

        public async Task<Guid> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = Book.Create(request.Title, request.Author, request.ISBN, request.PageCount, request.Format, request.ShelfId);

            _db.Books.Add(book);
            await _db.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}
