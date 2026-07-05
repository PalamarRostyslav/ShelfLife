namespace ShelfLife.Catalog.Domain.Entities
{
    public class Shelf
    {
        public Guid Id { get; private set; }

        public string? Name { get; private set; }

        public bool IsSystemShelf { get; private set; }

        private Shelf() { }

        public static Shelf CreateCustom(string name) => new() { Id = Guid.NewGuid(), Name = name, IsSystemShelf = false };

        public static Shelf CreateSystem(string name) => new() { Id = Guid.NewGuid(), Name = name, IsSystemShelf = true };
    }
}
