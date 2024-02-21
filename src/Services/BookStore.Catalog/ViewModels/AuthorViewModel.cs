using BookStore.Catalog.Domain.AuthorAggregate;

namespace BookStore.Catalog.ViewModels;

public sealed record AuthorViewModel(
    Guid Id,
    string Name,
    string ShortBio,
    AuthorContact AuthorContact
);
