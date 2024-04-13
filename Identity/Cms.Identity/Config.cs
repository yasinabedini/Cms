using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Cms.Identity;

public static class Config
{
    private static readonly IServiceProvider _serviceProvider;
    public static IConfiguration Configuration { get; private set; }

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
            new ApiScope("api.admin","Admin Api"),
            new ApiScope("api.site","Site Api"),
            };

    public static IEnumerable<Client> Clients(IConfiguration configuration)
    {
        Client[] clients =
        {
             new Client
             {
                ClientId = "site.client",
                ClientName = "Client site",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("secret".Sha256())},

                AllowedScopes = { "api.site" }
            },

            new Client
            {
                ClientId="admin",
                ClientName = "admin client",
                ClientSecrets = {
                    new Secret("mysecret".Sha256())
                },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris =
                {
                    $"https://localhost:5001/account/signin"
                },
                PostLogoutRedirectUris =
                {
                    $"https://localhost:5001/signout-callback-oidc"
                },
                AllowedScopes = new List<string>
                {
                    "openid",
                    "profile",
                    "api.admin",
                    "offline_access"
                },
                RequirePkce = true,
                AllowOfflineAccess = true
            }
        };

        return clients;
    }

}