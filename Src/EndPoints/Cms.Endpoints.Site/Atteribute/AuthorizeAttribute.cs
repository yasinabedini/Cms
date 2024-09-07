using Cms.Domain.Models.User.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Cms.Domain.Models.User.Repositories;
using Cms.Domain.Models.Token.Repositories;
using Cms.Infra.Models.Token.Repositories;

namespace Cms.Endpoints.Site.Atteribute
{
    public class AuthorizeTokenAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ITokenRepository tokenRepository = context.HttpContext.RequestServices.GetService<ITokenRepository>();
            IUserRepository userRepository = context.HttpContext.RequestServices.GetService<IUserRepository>();

            var token = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token)) context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

            else
            {
                token = token.Substring("Bearer ".Length).Trim();

                var phoneNumber = tokenRepository.GetPrincipalFromExpiredToken(token).Identity.Name;

                var user = userRepository.GetUserByPhoneNumber(phoneNumber);

                if (user == null) context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }
}
