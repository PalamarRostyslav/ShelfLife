namespace ShelfLife.Catalog.Domain.Entities
{
    public class Shelf
    {
        public Guid Id { get; private set; }

        public string? Name { get; private set; }

        public bool IsSystemShelf { get; private set; }

        public SystemShelfType? SystemShelfType { get; private set; }

        private Shelf() { }

        public static Shelf CreateCustom(string name) => 
                new() { Id = Guid.NewGuid(), Name = name, IsSystemShelf = false, SystemShelfType = null };

        public static Shelf CreateSystem(string name, SystemShelfType systemShelfType) => 
                new() { Id = Guid.NewGuid(), Name = name, IsSystemShelf = true, SystemShelfType = systemShelfType };

        public static Shelf CreateSystemWithId(Guid id, string name, SystemShelfType type) =>
                new() { Id = id, Name = name, IsSystemShelf = true, SystemShelfType = type };
    }
}
