using Ardalis.Specification.EntityFrameworkCore;
using BookStore.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence;

public sealed class Repository<TDbContext, TEntity>(TDbContext dbContext) : RepositoryBase<TEntity>(dbContext)
    where TEntity : class, IAggregateRoot
    where TDbContext : DbContext;