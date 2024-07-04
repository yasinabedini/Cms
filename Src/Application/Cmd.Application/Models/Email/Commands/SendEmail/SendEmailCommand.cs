using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Email.Commands.SendEmail
{
    public class SendEmailCommand:ICommand
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public SendEmailCommand(string toEmail, string subject, string message)
        {
            ToEmail = toEmail;
            Subject = subject;
            Message = message;
        }
    }
}
