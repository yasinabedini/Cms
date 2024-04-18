using IdentityModel.Client;

namespace Cms.Clients.AdminPanel.Auth
{
    public static class Token
    {
        public async static Task<TokenResponse> GetTokenResponse(HttpClient httpClient,HttpContext context)
        {
            IConfiguration configuration;

            configuration =  context.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

            var discovery = await httpClient.GetDiscoveryDocumentAsync(configuration.GetSection("AuthorityUrl").Value);
            if (discovery.IsError)
            {

            }
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = "admin",
                ClientSecret = "adminSecret",
                Scope = "api.admin"
            });
            if (tokenResponse.IsError)
            {

            }

            return tokenResponse;
        }
    }
}
