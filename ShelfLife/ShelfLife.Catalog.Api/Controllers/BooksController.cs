using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfLife.Catalog.Application.Books.Commands.AddBook;
using ShelfLife.Catalog.Application.Books.Queries.GetBookById;

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
    }
}
