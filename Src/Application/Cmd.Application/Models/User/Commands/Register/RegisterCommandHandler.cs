using Cmd.Application.Common.Commands;
using Cmd.Application.Tools;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, bool>
    {
        private readonly IUserRepository _repository;

        public RegisterCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _repository.GetUserByPhoneNumber(request.PhoneNumber);
            if (user is not null) return Task.FromResult(false);

            string hashPass = HashPassword.HashUsingSHA256(request.Password);

            var newUser = Cms.Domain.Models.User.Entities.User.Create(request?.FirstName, request?.LastName, request.PhoneNumber, hashPass);

            _repository.Add(newUser);
            _repository.Save();

            Tools.Sms.Model.Sms.SendSms(newUser.PhoneNumber.Value, $"خوش آمدید. کد احراز هویت شما {newUser.VerificationCode} است. موزه شهرداری اصفهان ");

            //TODO : Send Sms 

            return Task.FromResult(true);
        }
    }
}
