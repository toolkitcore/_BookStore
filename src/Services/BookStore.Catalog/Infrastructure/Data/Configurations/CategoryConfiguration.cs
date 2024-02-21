using BookStore.Catalog.Domain.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Catalog.Infrastructure.Data.Configurations;

public class CategoryConfiguration : BaseConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .HasMaxLength(200);

        builder.HasOne(e => e.ParentCategory)
            .WithMany(e => e.ChildrenCategories)
            .HasForeignKey(e => e.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
