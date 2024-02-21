using BookStore.Catalog.Domain.AuthorAggregate;
using BookStore.Catalog.Domain.BookAggregate;
using BookStore.Catalog.Domain.CategoryAggregate;
using BookStore.Catalog.Domain.PublisherAggregate;
using BookStore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Catalog.Infrastructure.Data;

public sealed class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : ApplicationDbContextBase(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Publisher> Publishers => Set<Publisher>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<BookCategory> BookCategories => Set<BookCategory>();
    public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
}
