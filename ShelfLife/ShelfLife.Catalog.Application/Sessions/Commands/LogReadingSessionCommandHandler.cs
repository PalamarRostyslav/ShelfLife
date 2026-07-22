using MediatR;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Domain.Entities;
using ShelfLife.SharedKernel.Exceptions;

namespace ShelfLife.Catalog.Application.Sessions.Commands
{
    public class LogReadingSessionCommandHandler : IRequestHandler<LogReadingSessionCommand, Guid>
    {
        private readonly IBookRepository _books;
        private readonly IReadingSessionRepository _sessions;
        private readonly IUnitOfWork _uow;

        public LogReadingSessionCommandHandler(IBookRepository bookRepository, IReadingSessionRepository sessionsRepository, IUnitOfWork uow)
        {
            _books = bookRepository;
            _sessions = sessionsRepository;
            _uow = uow;
        }

        public async Task<Guid> Handle(LogReadingSessionCommand request, CancellationToken cancellationToken)
        {
            _ = await _books.GetByIdAsync(request.BookId, cancellationToken) 
                ?? throw new NotFoundException(nameof(Books), request.BookId);

            var session = ReadingSession.Log(request.BookId, request.StartTime, request.PagesRead, request.MinutesSpent);
            _sessions.Add(session);
            await _uow.SaveChangesAsync(cancellationToken);

            return session.Id;
        }
    }
}
