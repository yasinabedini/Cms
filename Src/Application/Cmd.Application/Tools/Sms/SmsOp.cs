using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Tools.Sms
{
    public class SmsOp
    {
        public string ApimUrl { get; set; }
        public string TokenAddress { get; set; }
        public string SmsServiceUrl { get; set; }
        public string CustomerKey { get; set; }
        public string SecretKey { get; set; }
        public string ApiKey { get; set; }
        public string SenderLine { get; set; }
    }
}
