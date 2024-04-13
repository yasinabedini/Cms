using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Components.Web;

namespace Cms.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope("api.admin","admin api"),
                new ApiScope("api.site","site api")
            };

    public static IEnumerable<Client> Clients(IConfiguration configuration)
    {
        var clientList = new Client[]
            {
                new Client
                {
                    ClientId = "adminPanel",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes=
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api.admin"
                    },
                    RedirectUris = {$"{configuration.GetSection("AdminPanelUrl").Value}/signin-oidc"},
                    PostLogoutRedirectUris ={ $"{configuration.GetSection("AdminPanelUrl").Value}/signout-callback-oidc"},
                    AllowOfflineAccess = true,
                },

                new Client
                {
                    ClientId="site",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={"api.site"}
                },

                  new Client
                {
                    ClientId = "userPanel",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes=
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api.admin"
                    },
                    RedirectUris = { $"{configuration.GetSection("SiteUrl").Value}/signin-oidc"},
                    PostLogoutRedirectUris ={ $"{configuration.GetSection("SiteUrl").Value}/signout-callback-oidc"},
                    AllowOfflineAccess = true,
                },
            };
          
        return clientList;
    }
}