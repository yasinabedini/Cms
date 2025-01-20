using Cmd.Application.Common.Queries;
using Cmd.Application.Models.User.Queries.Common;
using Cmd.Application.Tools.Sms;
using Cms.Domain.Models.Sms.Repositories;
using Cms.Domain.Models.Token.Repositories;
using Cms.Domain.Models.User.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Queries.Inquire
{
    public class InquireUserQueryHandler : IQueryHandler<InquireUserQuery, InquireViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly ISmsRepository _smsRepository;
        private readonly IOptions<SmsOp> _appSettings;
        private readonly ITokenRepository _tokenRepository;
        private readonly HttpClient _httpClient;

        public InquireUserQueryHandler(IUserRepository repository, ISmsRepository smsRepository, IOptions<SmsOp> option, HttpClient httpClient, ITokenRepository tokenRepository)
        {
            _repository = repository;
            _smsRepository = smsRepository;
            _appSettings = option;
            _httpClient = httpClient;
            _tokenRepository = tokenRepository;
        }

        public async Task<InquireViewModel> Handle(InquireUserQuery request, CancellationToken cancellationToken)
        {
            int responseCode=200;
            bool hasAccount = false;
            bool hasPassword = false;
            bool phoneNumberConfirmed = false;
            string message = "";
            Random random = new Random();
            string code;
            code = random.Next(1111, 9999).ToString();

            if (!_repository.PhoneVerified(request.Mobile))
            {
                return await Task.FromResult(new InquireViewModel
                {
                    Message = "شماره موبایل را به درستی وارد کنید.",
                    ResponseCode = 400
                });
            }

            OtpViewModel otp = new OtpViewModel
            {
                Chanel = "",
                WaitingTime = 0,
                TrackingCode = ""
            };

            var user = _repository.GetUserByPhoneNumber(request.Mobile);

            if (user is not null)
            {
                hasAccount = true;

                phoneNumberConfirmed = user.PhoneConfirmed;

                if (!string.IsNullOrEmpty(user.Password))
                {
                    hasPassword = true;
                }

                code = user.GetVerificationCode();
                _repository.Update(user);
                _repository.Save();
            }

            string text = $". کد اعتبار سنجی شما {code} است. موزه تاریخ شهرداری اصفهان ";
            var sender = new Tools.Sms.Model.Sms(_appSettings,_httpClient,_tokenRepository);

            if (request.ForceOtp)
            {
                if (_smsRepository.BasicInquire(request.Mobile))
                {
                  //await  sender.SendSmsAsync(request.Mobile, text);
                    otp.Chanel = "sms";
                    otp.WaitingTime = 120;
                    _smsRepository.Add(new Cms.Domain.Models.Sms.Entities.Sms(request.Mobile, code, text));
                    _smsRepository.Save();
                    message = "پیامک برای شما ارسال گردید.";                    
                }
                else { message = "شما مجاز به ارسال 3 پیامک در هر دودقیقه هستید!"; responseCode = 400; }
            }

            if (/*_smsRepository.InquireSendSms(request.Mobile)*/true)
            {
                //await sender.SendSmsAsync(request.Mobile, text);
                otp.Chanel = "sms";
                otp.WaitingTime = 120;
                _smsRepository.Add(new Cms.Domain.Models.Sms.Entities.Sms(request.Mobile, code, text));
                _smsRepository.Save();
                message = "پیامک برای شما ارسال گردید.";
            }
            else { message = "شما هر دو دقیقه یک بار مجار به ارسال مجدد پیامک هستید."; responseCode = 400; }


            return await Task.FromResult(new InquireViewModel
            {
                HasAccount = hasAccount,
                HasPassword = hasPassword,
                Otp = otp,
                Message = message,
                ResponseCode = responseCode
            });
        }
    }
}
