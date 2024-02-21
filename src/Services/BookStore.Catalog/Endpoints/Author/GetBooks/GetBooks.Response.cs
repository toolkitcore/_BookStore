using Ardalis.Result;
using BookStore.Catalog.ViewModels;

namespace BookStore.Catalog.Endpoints.Author.GetBooks;

public class GetBooksResponse(
    List<AuthorViewModel> authors,
    PagedInfo pagedInfo)
{
    public List<AuthorViewModel> Authors { get; set; } = authors;
    public PagedInfo PagedInfo { get; set; } = pagedInfo;
}
