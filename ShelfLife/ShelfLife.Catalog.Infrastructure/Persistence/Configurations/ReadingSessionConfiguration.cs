using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfLife.Catalog.Domain.Entities;


namespace ShelfLife.Catalog.Infrastructure.Persistence.Configurations
{
    public class ReadingSessionConfiguration : IEntityTypeConfiguration<ReadingSession>
    {
        public void Configure(EntityTypeBuilder<ReadingSession> builder)
        {
            builder.ToTable("reading_sessions");
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.BookId);
            builder.Ignore(s => s.DomainEvents);
        }
    }
}
