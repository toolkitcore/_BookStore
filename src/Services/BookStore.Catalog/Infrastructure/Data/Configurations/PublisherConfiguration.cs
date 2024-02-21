using BookStore.Catalog.Domain.PublisherAggregate;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Catalog.Infrastructure.Data.Configurations;

public class PublisherConfiguration : BaseConfiguration<Publisher>
{
    public override void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.WebUrl)
            .HasMaxLength(200);
    }
}
