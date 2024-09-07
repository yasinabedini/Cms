using Cms.Domain.Common.Entities;
using Cms.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Sms.Entities
{
    public class Sms : AggregateRoot
    {
        public PhoneNumber PhoneNumber { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }

        public Sms(PhoneNumber phoneNumber, string code, string text)
        {
            PhoneNumber = phoneNumber;
            Code = code;
            Text = text;
        }


        public bool Inquire()
        {
            var result = CreateAt.AddMinutes(2) < DateTime.Now;
            return result;
        }
    }
}
