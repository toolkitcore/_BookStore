using BookStore.Catalog.Domain.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Catalog.Infrastructure.Data.Configurations;

public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
{
    public void Configure(EntityTypeBuilder<BookCategory> builder)
    {
        builder.HasKey(e => new { e.BookId, e.CategoryId });

        builder.HasOne(e => e.Book)
            .WithMany(e => e.BookCategories)
            .HasForeignKey(e => e.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Category)
            .WithMany(e => e.BookCategories)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
