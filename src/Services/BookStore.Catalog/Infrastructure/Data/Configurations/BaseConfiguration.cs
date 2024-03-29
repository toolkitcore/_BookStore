﻿using BookStore.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Catalog.Infrastructure.Data.Configurations;

public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CreatedDate)
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(e => e.UpdateDate)
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(e => e.Version)
            .IsConcurrencyToken();
    }
}
