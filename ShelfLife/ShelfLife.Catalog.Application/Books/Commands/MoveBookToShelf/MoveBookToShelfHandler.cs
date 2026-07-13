using MediatR;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Exceptions;

namespace ShelfLife.Catalog.Application.Books.Commands.MoveBookToShelf
{
    public class MoveBookToShelfHandler : IRequestHandler<MoveBookToShelfCommand>
    {
        private readonly IBookRepository _books;
        private readonly IUnitOfWork _uow;
        private readonly IShelfRepository _shelves;

        public MoveBookToShelfHandler(IBookRepository books, IShelfRepository shelves, IUnitOfWork uow)
        {
            _books = books;
            _shelves = shelves;
            _uow = uow;
        }

        public async Task Handle(MoveBookToShelfCommand request, CancellationToken cancellationToken)
        {
            var book = await _books.GetByIdAsync(request.BookId, cancellationToken) 
                ?? throw new NotFoundException(nameof(Book), request.BookId);

            var shelf = await _shelves.GetByIdAsync(request.ShelfId, cancellationToken) 
                ?? throw new NotFoundException(nameof(Shelf), request.ShelfId);

            book.MoveToShelf(shelf);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
