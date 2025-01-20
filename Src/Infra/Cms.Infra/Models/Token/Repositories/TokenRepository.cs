using Cms.Domain.Models.Token.Entities;
using Cms.Domain.Models.Token.Repositories;
using Cms.Domain.Models.User.Repositories;
using Cms.Infra.Common.Auth;
using Cms.Infra.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Token.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly JwtOptions _appSettings;
        private readonly IUserRepository _repository;
        private readonly CmsDbContext _context;

        public TokenRepository(IOptions<JwtOptions> appSettings, IUserRepository repository, CmsDbContext context)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
            _context = context;
        }

        public Domain.Models.Token.Entities.Token GenerateToken(string userName)
        {
            return GenerateJWTTokens(userName);
        }

        public Domain.Models.Token.Entities.Token GenerateRefreshToken(string username)
        {
            return GenerateJWTTokens(username);
        }

        public Domain.Models.Token.Entities.Token GenerateJWTTokens(string userName)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
new Claim(ClaimTypes.Name, userName)
                  }),
                    Expires = DateTime.Now.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new Domain.Models.Token.Entities.Token("Bearer", tokenHandler.WriteToken(token), 54600, refreshToken);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_appSettings.Secret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public void AddApiToken(string access_token, string scope, string token_type, int expires_in)
        {
            _context.ApiTokens.Add(new ApiToken
            {
                access_token = access_token,
                expires_in = expires_in,
                scope = scope,
                token_type = token_type,
                CreateAt = DateTime.Now,
                IsDelete = false,
                IsEnable = true
            });
            _context.SaveChanges();
        }

        public bool ApiTokenAvailable()
        {
            var token = _context.ApiTokens.OrderBy(t => t.CreateAt).LastOrDefault();

            if (token is null)
            {
                return false;
            }

            var expireDate = token.CreateAt.AddSeconds(token.expires_in);

            if (expireDate <= DateTime.Now)
            {
                return false;
            }
            else if (expireDate > DateTime.Now)
            {
                return true;
            }
            else
            {
                return true;
            }            
        }

        public ApiToken GetApiToken()
        {
            return _context.ApiTokens.OrderBy(t => t.CreateAt).Last();
        }
    }
}
