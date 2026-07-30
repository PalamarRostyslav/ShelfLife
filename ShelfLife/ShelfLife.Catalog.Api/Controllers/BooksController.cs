using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfLife.Catalog.Application.Books.Commands.AddBook;
using ShelfLife.Catalog.Application.Books.Commands.DeleteBook;
using ShelfLife.Catalog.Application.Books.Commands.MoveBookToShelf;
using ShelfLife.Catalog.Application.Books.Commands.UpdateBook;
using ShelfLife.Catalog.Application.Books.Queries.GetBookById;
using ShelfLife.Catalog.Application.Books.Queries.GetBooks;
using ShelfLife.Catalog.Application.Books.Queries.GetSessionsForBook;
using ShelfLife.Catalog.Application.Sessions.Commands;
using ShelfLife.Catalog.Domain.Entities;

namespace ShelfLife.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ISender _sender;

        public BooksController(ISender sender) => _sender = sender;

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookCommand command, CancellationToken cancellationToken)
        {
            var bookId = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = bookId }, new { id = bookId });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var book = await _sender.Send(new GetBookByIdQuery(id), cancellationToken);

            return book is null ? NotFound() : Ok(book);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] Guid? shelfId, [FromQuery] int skip = 0, [FromQuery] int take = 50, CancellationToken ct = default)
        {
            var books = await _sender.Send(new GetBooksQuery(shelfId, skip, take), ct);

            return Ok(books);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, UpdateBookRequest request, CancellationToken ct)
        {
            await _sender.Send(new UpdateBookCommand(id, request.Title, request.Author, request.Isbn,
                request.PageCount, request.Format, request.Rating), ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken ct)
        {
            await _sender.Send(new DeleteBookCommand(id), ct);
            return NoContent();
        }

        [HttpPost("{id:guid}/shelf")]
        public async Task<IActionResult> MoveToShelf(Guid id, MoveToShelfRequest request, CancellationToken ct)
        {
            await _sender.Send(new MoveBookToShelfCommand(id, request.ShelfId), ct);
            return NoContent();
        }

        [HttpPost("{id:guid}/sessions")]
        public async Task<IActionResult> LogSession(Guid id, LogSessionRequest request, CancellationToken ct)
        {
            var sessionId = await _sender.Send(new LogReadingSessionCommand(id, request.Date, request.PagesRead, request.MinutesSpent), ct);
            return CreatedAtAction(nameof(GetSessions), new { id }, new { id = sessionId });
        }

        [HttpGet("{id:guid}/sessions")]
        public async Task<IActionResult> GetSessions(Guid id, CancellationToken ct)
        {
            var sessions = await _sender.Send(new GetSessionsForBookQuery(id), ct);
            return Ok(sessions);
        }
    }

    public record UpdateBookRequest(string Title, string Author, string? Isbn, int PageCount, BookFormat Format, int? Rating);
    public record MoveToShelfRequest(Guid ShelfId);
    public record LogSessionRequest(DateTime Date, int PagesRead, int MinutesSpent);
}
