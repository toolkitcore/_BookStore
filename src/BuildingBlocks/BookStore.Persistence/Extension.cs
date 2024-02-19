using Ardalis.GuardClauses;
using BookStore.Core.SharedKernel;
using BookStore.Persistence.Interceptors;
using EntityFramework.Exceptions.PostgreSQL;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookStore.Persistence;

public static class Extensions
{
    public static IServiceCollection AddPostgresDbContext<TDbContext>(
        this IServiceCollection services,
        string connString,
        Action<DbContextOptionsBuilder>? doMoreDbContextOptionsConfigure = null,
        Action<IServiceCollection>? doMoreActions = null)
        where TDbContext : DbContext, IDatabaseFacade, IDomainEventContext
    {
        Guard.Against.Null(connString, message: "Connection string is required");

        services.AddScoped<IDbCommandInterceptor, TimingInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContextPool<TDbContext>((sp, options) =>
        {
            options.UseNpgsql(connString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                })
                .UseExceptionProcessor()
                .EnableServiceProviderCaching()
                .UseSnakeCaseNamingConvention()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            options.AddInterceptors(sp.GetServices<IDbCommandInterceptor>());
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            if (string.Equals(Environments.Development, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                    StringComparison.OrdinalIgnoreCase))
                options.EnableDetailedErrors()
                    .EnableSensitiveDataLogging();

            doMoreDbContextOptionsConfigure?.Invoke(options);
        });

        services.AddScoped<IDatabaseFacade>(provider =>
            provider.GetService<TDbContext>() ?? throw new InvalidOperationException());
        services.AddScoped<IDomainEventContext>(provider =>
            provider.GetService<TDbContext>() ?? throw new InvalidOperationException());

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        doMoreActions?.Invoke(services);

        return services;
    }
}