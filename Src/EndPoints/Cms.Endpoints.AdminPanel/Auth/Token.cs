using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cms.Endpoints.AdminPanel.Pages.Common;
using Cms.Endpoints.AdminPanel.Pages.NewsType;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Cms.Endpoints.AdminPanel.Auth
{
    public static class Token
    {
        public async static Task<string> GetTokenResponse(HttpClient httpClient, HttpContext context)
        {
            IConfiguration configuration;

            configuration = context.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

            var secret = configuration.GetSection("SecretKey").Value;

            var data = new { secretKey = secret };
            var jsonInString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response =await httpClient.PostAsync("/api/Token/GetToken", content);
            var result = await response.Content.ReadAsStringAsync();


            return result;
        }

        public static string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gvbhnjmkmjenhbcnjmedyhbnjwdeuhybnjwjduehikecmvjn"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentials,
                Issuer = "museum_admin_api",
                Audience = "https://newadminpanel.pajal.net/"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
