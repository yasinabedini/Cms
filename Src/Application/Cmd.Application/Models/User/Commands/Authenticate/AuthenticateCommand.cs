using Cmd.Application.Common.Commands;
using Cmd.Application.Models.User.Queries.Common;
using Cms.Domain.Models.Token.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.Authenticate
{
    public class AuthenticateCommand:ICommand<TokenViewModel>
    {
        public string Mobile { get; set; }
        public string Code { get; set; }
        public string? TrackingCode { get; set; }

        public AuthenticateCommand(string mobile, string code, string? trackingCode)
        {
            Mobile = mobile;
            Code = code;
            TrackingCode = trackingCode;
        }
    }
}
