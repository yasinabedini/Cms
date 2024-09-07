using Azure.Core.Pipeline;
using Cmd.Application.Common.Commands;
using Cmd.Application.Common.Queries;
using Cmd.Application.Models.User.Queries.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Queries.Inquire
{
    public class InquireUserQuery : IQuery<InquireViewModel>
    {
        public bool ForceOtp { get; set; }
        public string Mobile { get; set; }
    }
}
