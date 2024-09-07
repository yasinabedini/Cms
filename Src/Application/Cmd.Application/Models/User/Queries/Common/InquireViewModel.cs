using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Queries.Common
{
    public class InquireViewModel
    {
        public bool HasAccount { get; set; }
        public bool HasPassword { get; set; }        
        public string Message { get; set; }
        public int ResponseCode { get; set; }
        public OtpViewModel Otp { get; set; }
    }
}
