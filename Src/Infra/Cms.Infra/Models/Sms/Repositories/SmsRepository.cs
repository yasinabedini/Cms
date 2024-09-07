using Cms.Domain.Models.Sms.Repositories;
using Cms.Infra.Common.Repository;
using Cms.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Sms.Repositories
{
    public class SmsRepository : BaseRepository<Domain.Models.Sms.Entities.Sms>, ISmsRepository
    {
        private readonly CmsDbContext _context;
        public SmsRepository(CmsDbContext context) : base(context)
        {
            _context = context;
        }

        public bool BasicInquire(string phoneNumber)
        {
            //var count = _context.Sms.Count(c => c.CreateAt > DateTime.Now - TimeSpan.FromMinutes(2) && c.PhoneNumber == new Domain.Common.ValueObjects.PhoneNumber(phoneNumber));
            //return count <= 3;
            return true;
        }

        public Domain.Models.Sms.Entities.Sms GetLastSms(string phoneNumber)
        {
            return _context.Sms.OrderBy(t => t.Id).LastOrDefault(t => t.PhoneNumber == new Domain.Common.ValueObjects.PhoneNumber(phoneNumber));
        }

        public bool InquireSendSms(string phoneNumber)
        {
            if (BasicInquire(phoneNumber))
            {
                var sms = GetLastSms(phoneNumber);

                if (sms is not null)
                {
                    return sms.Inquire();
                }
                else { return true; }
            }
            return false;
        }

        public bool SendSms(string phoneNumber, string text)
        {
            throw new NotImplementedException();
        }

        public bool CheckCode(string phoneNumber, string code) 
        {
            var sms = GetLastSms(phoneNumber);
            var result = sms?.Code == code;

            if (sms is not null&&result) 
            {
                sms.IsDelete = true;
                Update(sms);
                Save();
            }

            return result;
        }
    }
}
