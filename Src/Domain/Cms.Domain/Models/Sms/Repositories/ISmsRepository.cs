using Cms.Domain.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Sms.Repositories
{
    public interface ISmsRepository:IRepository<Sms.Entities.Sms>
    {
        bool InquireSendSms(string phoneNumber);
        bool SendSms(string phoneNumber,string text);
        Sms.Entities.Sms GetLastSms(string phoneNumber);
        bool BasicInquire(string phoneNumber);
        bool CheckCode(string phoneNumber, string code);
    }
}
