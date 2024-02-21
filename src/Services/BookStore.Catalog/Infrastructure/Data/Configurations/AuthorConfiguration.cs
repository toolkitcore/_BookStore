using BookStore.Catalog.Domain.AuthorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Catalog.Infrastructure.Data.Configurations;

public class AuthorConfiguration : BaseConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ShortBio)
            .HasMaxLength(500);

        builder.Property(e => e.AuthorContact)
            .IsUnicode()
            .HasColumnType("jsonb");
    }
}
