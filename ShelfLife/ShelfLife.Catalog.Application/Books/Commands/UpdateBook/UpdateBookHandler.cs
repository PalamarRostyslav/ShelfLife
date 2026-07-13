using MediatR;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Exceptions;

namespace ShelfLife.Catalog.Application.Books.Commands.UpdateBook
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _books;
        private readonly IUnitOfWork _uow;

        public UpdateBookHandler(IBookRepository books, IUnitOfWork uow)
        {
            _books = books;
            _uow = uow;
        }

        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _books.GetByIdAsync(request.BookId, cancellationToken)
                ?? throw new NotFoundException(nameof(Book), request.BookId);

            book.UpdateDetails(request.Title, request.Author, request.ISBN, request.PageCount, request.Format, request.Rating);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
