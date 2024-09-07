using Cms.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Token.Entities
{
    public class Token
    {
        public int Id { get; set; }

        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public bool IsDelete { get; set; }

        public Token()
        {
        }

        public Token(string tokenType, string accessToken, long expiresIn, string refreshToken)
        {
            TokenType = tokenType;
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            RefreshToken = refreshToken;
        }
    }
}
