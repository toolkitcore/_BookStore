using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace BookStore.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new("cart", "Access to Cart API"),
        new("ordering", "Access to Ordering API"),
        new("aggregator", "Access to Aggregator API"),
        new("payment", "Access to Payment API")
    ];

    public static IEnumerable<ApiResource> ApiResources =>
    [
        new("cart-api", "Cart API")
        {
            Scopes = { "cart" }
        },
        new("ordering-api", "Ordering API")
        {
            Scopes = { "ordering" }
        },
        new("aggregator-api", "Aggregator API")
        {
            Scopes = { "aggregator" }
        },
        new("payment-api", "Payment API")
        {
            Scopes = { "payment" }
        }
    ];

    public static IEnumerable<Client> Clients(IConfiguration configuration) =>
    [
        new()
        {
            ClientId = "website",
            ClientName = "Website Front-end",
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,
            RequireConsent = false,
            AllowedCorsOrigins = { configuration["WebsiteClientUrlExternal"] },
            RedirectUris = { $"{configuration["WebsiteClientUrlExternal"]}/authentication/login-callback" },
            PostLogoutRedirectUris =
            {
                $"{configuration["WebsiteClientUrlExternal"]}/authentication/logout-callback"
            },

            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "cart",
                "ordering",
                "aggregator",
                "payment"
            },
        },
        new()
        {
            ClientId = "cartswaggerui",
            ClientName = "Cart Swagger UI",
            AllowedGrantTypes = GrantTypes.Implicit,
            AllowAccessTokensViaBrowser = true,
            RedirectUris = { $"{configuration["CartApiUrlExternal"]}/swagger/oauth2-redirect.html" },
            PostLogoutRedirectUris = { $"{configuration["CartApiUrlExternal"]}/swagger/" },
            AllowedScopes =
            {
                "cart"
            }
        },
        new()
        {
            ClientId = "orderingswaggerui",
            ClientName = "Ordering Swagger UI",
            AllowedGrantTypes = GrantTypes.Implicit,
            AllowAccessTokensViaBrowser = true,
            RedirectUris = { $"{configuration["OrderingApiUrlExternal"]}/swagger/oauth2-redirect.html" },
            PostLogoutRedirectUris = { $"{configuration["OrderingApiUrlExternal"]}/swagger/" },
            AllowedScopes =
            {
                "ordering"
            }
        },
        new()
        {
            ClientId = "aggregatorswaggerui",
            ClientName = "Aggregator Swagger UI",
            AllowedGrantTypes = GrantTypes.Implicit,
            AllowAccessTokensViaBrowser = true,
            RedirectUris = { $"{configuration["AggregatorApiUrlExternal"]}/swagger/oauth2-redirect.html" },
            PostLogoutRedirectUris = { $"{configuration["AggregatorApiUrlExternal"]}/swagger/" },
            AllowedScopes =
            {
                "cart",
                "payment",
                "aggregator"
            }
        },
        new()
        {
            ClientId = "paymentswaggerui",
            ClientName = "Payment Swagger UI",
            AllowedGrantTypes = GrantTypes.Implicit,
            AllowAccessTokensViaBrowser = true,
            RedirectUris = { $"{configuration["PaymentApiUrlExternal"]}/swagger/oauth2-redirect.html" },
            PostLogoutRedirectUris = { $"{configuration["PaymentApiUrlExternal"]}/swagger/" },
            AllowedScopes =
            {
                "payment"
            }
        }
    ];
}