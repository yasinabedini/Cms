using Cmd.Application.Common.Commands;
using Cmd.Application.Tools.Sms;
using Cms.Domain.Models.Token.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sms.Commands
{
    public class SendSmsCommandHandler : ICommandHandler<SendSmsCommand>
    {
        private readonly IOptions<SmsOp> _appSettings;
        private readonly ITokenRepository _tokenRepository;
        private readonly HttpClient _httpClient;

        public SendSmsCommandHandler(IOptions<SmsOp> option, HttpClient httpClient, ITokenRepository tokenRepository)
        {
            _appSettings = option;
            _httpClient = httpClient;
            _tokenRepository = tokenRepository;
        }


        public async Task Handle(SendSmsCommand request, CancellationToken cancellationToken)
        {
            var sender = new Tools.Sms.Model.Sms(_appSettings, _httpClient,_tokenRepository);

            await sender.SendSmsAsync(request.PhoneNumber, request.Text);            
        }
    }
}
