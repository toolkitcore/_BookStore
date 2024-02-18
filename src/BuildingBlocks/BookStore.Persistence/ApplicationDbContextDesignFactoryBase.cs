using Ardalis.GuardClauses;
using BookStore.Core.Helpers;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookStore.Persistence;

public class ApplicationDbContextDesignFactoryBase<TDbContext>(string dbName)
    : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : DbContext
{
    public TDbContext CreateDbContext(string[] args)
    {
        var connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)
            .GetConnectionString(dbName);

        Guard.Against.NullOrEmpty(connString, dbName);

        var optionsBuilder = new DbContextOptionsBuilder<TDbContext>()
            .UseNpgsql(connString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                }
            )
            .UseExceptionProcessor()
            .EnableServiceProviderCaching()
            .UseSnakeCaseNamingConvention()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options)!;
    }
}