﻿using Cms.Domain.Models.Token.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Token.Repositories
{
    public interface ITokenRepository
    {
        Entities.Token GenerateToken(string userName);
        Entities.Token GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        void AddApiToken(string access_token,string scope,string token_type, int expires_in);
        bool ApiTokenAvailable();
        ApiToken GetApiToken();
    }
}
