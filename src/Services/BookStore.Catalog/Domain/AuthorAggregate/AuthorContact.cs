using BookStore.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Catalog.Domain.AuthorAggregate;

[Owned]
public class AuthorContact(string email, string? phone, string? webUrl) : ValueObject
{
    public string Email { get; set; } = email;
    public string? Phone { get; set; } = phone;
    public string? WebUrl { get; set; } = webUrl;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
        yield return Phone ?? string.Empty;
        yield return WebUrl ?? string.Empty;
    }
}
