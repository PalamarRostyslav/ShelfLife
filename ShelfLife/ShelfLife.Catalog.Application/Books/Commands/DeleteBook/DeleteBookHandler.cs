using MediatR;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Exceptions;

namespace ShelfLife.Catalog.Application.Books.Commands.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _books;
        private readonly IUnitOfWork _uow;

        public DeleteBookHandler(IBookRepository books, IUnitOfWork uow)
        {
            _books = books;
            _uow = uow;
        }

        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _books.GetByIdAsync(request.BookId, cancellationToken)
                ?? throw new NotFoundException(nameof(Book), request.BookId);

            _books.Remove(book);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
