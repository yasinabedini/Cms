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
                    ClientId="admin",
                    ClientSecrets = {new Secret("adminSecret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={"api.admin"}
                },

                new Client
                {
                    ClientId="site",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={"api.site"}
                }
            };

        return clientList;
    }
}