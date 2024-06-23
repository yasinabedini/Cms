using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sms.Commands
{
    public class SendSmsCommand:ICommand
    {
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
    }
}
