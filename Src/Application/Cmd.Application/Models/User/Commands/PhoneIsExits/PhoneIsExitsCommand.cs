using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.PhoneIsExits
{
    public class PhoneIsExitsCommand:ICommand<bool>
    {
        public string PhoneNumber { get; set; }

        public PhoneIsExitsCommand()
        {
            
        }
        public PhoneIsExitsCommand(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
