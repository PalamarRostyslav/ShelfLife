using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfLife.Catalog.Domain.Entities;

namespace ShelfLife.Catalog.Infrastructure.Persistence.Configurations
{
    public class ShelfConfiguration : IEntityTypeConfiguration<Shelf>
    {
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
            builder.ToTable("shelves");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Property(s => s.SystemShelfType).HasConversion<string>();

            builder.HasData(
                Shelf.CreateSystemWithId(new Guid("11111111-1111-1111-1111-111111111111"), "To Read", SystemShelfType.ToRead),
                Shelf.CreateSystemWithId(new Guid("22222222-2222-2222-2222-222222222222"), "Reading", SystemShelfType.Reading),
                Shelf.CreateSystemWithId(new Guid("33333333-3333-3333-3333-333333333333"), "Finished", SystemShelfType.Finished),
                Shelf.CreateSystemWithId(new Guid("44444444-4444-4444-4444-444444444444"), "DNF", SystemShelfType.Dnfs)
            );
        }
    }
}
