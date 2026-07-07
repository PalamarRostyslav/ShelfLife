using MediatR;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Domain.Entities;

namespace ShelfLife.Catalog.Application.Books.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Guid>
    {
        private readonly IBookRepository _books;
        private readonly IUnitOfWork _uow;

        public AddBookCommandHandler(IBookRepository books, IUnitOfWork uow)
        {
            _books = books;
            _uow = uow;
        }

        public async Task<Guid> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = Book.Create(request.Title, request.Author, request.ISBN, request.PageCount, request.Format, request.ShelfId);

            _books.Add(book);
            await _uow.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}
