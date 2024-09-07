using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Queries.Common
{
    public class TokenViewModel
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
        public int ResponseCode { get; set; }
    }
}
