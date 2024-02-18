using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BookStore.Persistence;

public interface IDatabaseFacade
{
    public DatabaseFacade Database { get; }
}
