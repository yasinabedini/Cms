using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sms.Commands
{
    public class SendSmsCommandHandler : ICommandHandler<SendSmsCommand>
    {
        public Task Handle(SendSmsCommand request, CancellationToken cancellationToken)
        {
            Tools.Sms.Model.Sms.SendSms(request.PhoneNumber, request.Text);

            return Task.CompletedTask;
        }
    }
}
