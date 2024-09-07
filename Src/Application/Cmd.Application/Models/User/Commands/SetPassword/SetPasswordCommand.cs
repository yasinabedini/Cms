using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.SetPassword
{
    public class SetPasswordCommand:ICommand
    {
        public string PhoneNumber { get; set; }
        public string Passwrod { get; set; }
    }
}
