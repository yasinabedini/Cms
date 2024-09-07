using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Token.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.RefreshToken
{
    public class RefreshTokenCommand : ICommand<Token>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
