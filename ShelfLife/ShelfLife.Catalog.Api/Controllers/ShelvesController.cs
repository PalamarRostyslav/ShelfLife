using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfLife.Catalog.Application.Shelves.Commands;
using ShelfLife.Catalog.Application.Shelves.Queries;

namespace ShelfLife.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/shelves")]
    public class ShelvesController : ControllerBase
    {
        private readonly ISender _sender;

        public ShelvesController(ISender sender) => _sender = sender;

        [HttpGet]
        public async Task<IActionResult> GetShelves(CancellationToken cancellationToken) => Ok(await _sender.Send(new GetShelvesQuery(), cancellationToken));

        [HttpPost]
        public async Task<IActionResult> CreateShelf(CreateShelfCommand command, CancellationToken cancellationToken)
        {
            var shelfId = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetShelves), new { id = shelfId }, new { id = shelfId });
        }
    }
}
