using Ardalis.Specification;

namespace BookStore.Catalog.Domain.AuthorAggregate.Specifications;

public sealed class AuthorByIdSpec : Specification<Author>
{
    public AuthorByIdSpec(Guid authorId)
    {
        Query
            .Where(a => a.Id.Equals(authorId));
    }
}
