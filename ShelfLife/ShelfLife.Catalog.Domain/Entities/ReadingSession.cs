using ShelfLife.SharedKernel.Events;

namespace ShelfLife.Catalog.Domain.Entities
{
    public class ReadingSession
    {
        public Guid Id { get; private set; }

        public Guid BookId { get; private set; }

        public DateTime Date { get; private set; }

        public int PagesRead { get; private set; }

        public int MinutesSpent { get; private set; }

        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private ReadingSession() { }

        public static ReadingSession Log(Guid bookId, DateTime date, int pagesRead, int minutesSpent)
        {
            var session = new ReadingSession
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                Date = date,
                PagesRead = pagesRead,
                MinutesSpent = minutesSpent
            };

            session._domainEvents.Add(new SessionLogged(session.Id, bookId, date, pagesRead));
            return session;
        }

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
