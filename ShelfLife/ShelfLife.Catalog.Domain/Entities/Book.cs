using ShelfLife.SharedKernel.Events;

namespace ShelfLife.Catalog.Domain.Entities
{
    public class Book : IHasDomainEvents
    {
        public Guid Id { get; private set; }

        public string? Title { get; private set; }

        public string? Author { get; private set; }

        public string? ISBN { get; private set; }

        public int PageCount { get; private set; }

        public BookFormat Format { get; private set; }

        public int? Rating { get; private set; }

        public Guid ShelfId { get; private set; }

        public DateTime AddedAt { get; private set; }

        public DateTime? FinishedAt { get; private set; }

        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private Book() { }

        public static Book Create(string title, string author, string isbn, int pageCount, BookFormat format, Guid shelfId)
        {
            return new Book
            {
                Id = Guid.NewGuid(),
                Title = title,
                Author = author,
                ISBN = isbn,
                PageCount = pageCount,
                Format = format,
                ShelfId = shelfId,
                AddedAt = DateTime.UtcNow
            };
        }

        public void MoveToShelf(Guid newShelfId, bool isFinishedShelf)
        {
            ShelfId = newShelfId;
            if (isFinishedShelf)
            {
                FinishedAt = DateTime.UtcNow;
                _domainEvents.Add(new BookFinished(Id, DateTime.UtcNow));
            }
        }

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}