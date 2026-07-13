using MediatR;
using ShelfLife.Catalog.Application.Books.DTOs;
using ShelfLife.Catalog.Application.Books.Specifications;
using ShelfLife.Catalog.Application.Common.Interfaces;

namespace ShelfLife.Catalog.Application.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _books;

        public GetBooksQueryHandler(IBookRepository books)
        {
            _books = books;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var spec = new BooksFilterSpec(request.ShelfId, request.Skip, request.Take);
            var books = await _books.ListAsync(spec, cancellationToken);

            return books.Select(b => new BookDto(b.Id, b.Title!, b.Author!, b.ISBN, b.PageCount,
                b.Format.ToString(), b.Rating, b.ShelfId, b.AddedAt, b.FinishedAt)).ToList();
        }
    }
}
