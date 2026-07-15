using MediatR;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Domain.Entities;

namespace ShelfLife.Catalog.Application.Shelves.Commands
{
    public class CreateShelfCommandHandler : IRequestHandler<CreateShelfCommand, Guid>
    {
        private readonly IShelfRepository _shelves;
        private readonly IUnitOfWork _uow;

        public CreateShelfCommandHandler(IShelfRepository shelves, IUnitOfWork uow)
        {
            _shelves = shelves;
            _uow = uow;
        }

        public async Task<Guid> Handle(CreateShelfCommand request, CancellationToken cancellationToken)
        {
            var shelf = Shelf.CreateCustom(request.Name);
            _shelves.Add(shelf);
            await _uow.SaveChangesAsync(cancellationToken);

            return shelf.Id;
        }
    }
}
