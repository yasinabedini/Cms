using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.ConfirmPhone
{
    public class ConfirmPhoneCommand : ICommand<bool>
    {
        public string PhoneNumber { get; set; }
        public string Code { get; set; }

        public ConfirmPhoneCommand()
        {
            
        }
        public ConfirmPhoneCommand(string phoneNumber, string code)
        {
            PhoneNumber = phoneNumber;
            Code = code;
        }
    }
}
