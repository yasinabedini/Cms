using Cmd.Application.Common.Commands;
using Cmd.Application.Tools.Email;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Email.Commands.SendEmail
{
    public class SendEmailCommandHandler : ICommandHandler<SendEmailCommand>
    {
        private readonly IOptions<EmailOp> _option;

        public SendEmailCommandHandler(IOptions<EmailOp> option)
        {
            _option = option;
        }

        public Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var sender = new Tools.Email.SendEmail(_option);
            sender.Send(request.ToEmail, request.Subject, request.Message);

            return Task.CompletedTask;
        }
    }
}
