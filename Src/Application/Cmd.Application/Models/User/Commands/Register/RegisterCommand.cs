using Cmd.Application.Common.Commands;
using Cmd.Application.Models.User.Queries.Common;
using Cms.Domain.Models.Token.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.Register
{
    public class RegisterCommand:ICommand<TokenViewModel>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Otp { get; set; }
        public string? Email { get; set; }
        public string? Study { get; set; }
        public long? DegreeId { get; set; }

        public RegisterCommand(string? firstName, string? lastName, string phoneNumber, string? email, string otp, string? study, long? degreeId)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Otp = otp;
            Study = study;
            DegreeId = degreeId;
        }
    }
}
