using BookStore.Catalog.Domain.BookAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Catalog.Infrastructure.Data.Configurations;

public sealed class BookConfiguration : BaseConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .HasMaxLength(200);

        builder.Property(e => e.Price)
            .HasDefaultValue(0)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.PictureFileName)
            .HasMaxLength(200);

        builder.HasMany(e => e.BookAuthors)
            .WithOne(e => e.Book)
            .HasForeignKey(e => e.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.BookCategories)
            .WithOne(e => e.Book)
            .HasForeignKey(e => e.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Publisher)
            .WithMany(e => e.Books)
            .HasForeignKey(b => b.PublisherId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
