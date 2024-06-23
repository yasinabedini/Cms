using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.CheckUserIsExits
{
    public class CheckUserIsExitsCommand:ICommand<bool>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public CheckUserIsExitsCommand()
        {
            
        }
        public CheckUserIsExitsCommand(string phoneNumber, string password)
        {
            PhoneNumber = phoneNumber;
            Password = password;
        }
    }
}
