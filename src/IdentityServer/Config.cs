using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client
            {
                ClientId = "movieClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "movieAPI" }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("movieAPI", "Movie API")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[] { };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[] { };

    public static List<TestUser> TestUsers =>
        new List<TestUser> { };
}