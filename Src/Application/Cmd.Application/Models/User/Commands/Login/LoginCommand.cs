﻿using Cmd.Application.Common.Commands;
using Cmd.Application.Models.User.Queries.Common;
using Cms.Domain.Models.Token.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.Login
{
    public class LoginCommand:ICommand<TokenViewModel>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public LoginCommand()
        {
            
        }
        public LoginCommand(string phoneNumber, string password)
        {
            PhoneNumber = phoneNumber;
            Password = password;
        }
    }
}
