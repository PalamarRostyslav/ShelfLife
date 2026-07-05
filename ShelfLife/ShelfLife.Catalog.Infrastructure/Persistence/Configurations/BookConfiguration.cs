using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfLife.Catalog.Domain.Entities;

namespace ShelfLife.Catalog.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("books");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(500);
            builder.Property(b => b.Author).IsRequired().HasMaxLength(300);
            builder.Property(b => b.ISBN).HasMaxLength(20);
            builder.Property(b => b.Format).HasConversion<string>();
            builder.HasIndex(b => b.ShelfId);
            builder.Ignore(b => b.DomainEvents);
        }
    }
}
