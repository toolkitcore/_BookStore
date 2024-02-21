using Ardalis.Specification;

namespace BookStore.Catalog.Domain.CategoryAggregate.Specifications;

public sealed class CategoryByIdSpec : Specification<Category>
{
    public CategoryByIdSpec(Guid categoryId)
    {
        Query
            .Where(category => category.Id == categoryId);
    }
}
