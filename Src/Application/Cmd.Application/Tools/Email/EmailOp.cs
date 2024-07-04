using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Tools.Email
{
    public class EmailOp
    {
        public string MailServer { get; set; }
        public string EmailSubject { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }

    }
}
