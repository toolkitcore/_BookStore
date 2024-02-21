using BookStore.Catalog.Domain.AuthorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Catalog.Infrastructure.Data.Configurations;

public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        builder.HasKey(e => new { e.BookId, e.AuthorId });

        builder.HasOne(e => e.Book)
            .WithMany(e => e.BookAuthors)
            .HasForeignKey(e => e.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Author)
            .WithMany(e => e.BookAuthors)
            .HasForeignKey(e => e.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
