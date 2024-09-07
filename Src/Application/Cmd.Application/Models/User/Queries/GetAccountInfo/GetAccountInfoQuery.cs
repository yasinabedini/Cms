using Cmd.Application.Common.Queries;
using Cmd.Application.Models.User.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Queries.GetAccountInfo
{
    public class GetAccountInfoQuery:IQuery<UserViewModel>
    {
        public string Token { get; set; }
    }
}
