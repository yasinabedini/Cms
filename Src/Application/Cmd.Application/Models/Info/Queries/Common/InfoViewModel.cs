using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Queries.Common
{
    public class InfoViewModel
    {
        public long Id { get; set; }
        public string Address { get; private set; }
        public string WorkTime { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }
        public string InstagramAddress { get; private set; }
        public string EitaaAddress { get; set; }
        public long LanguageId { get; set; }

        public InfoViewModel(long id,string address, string workTime, string phoneNumber, string emailAddress, string instagramAddress, long languageId, string eitaaAddress)
        {
            Address = address;
            WorkTime = workTime;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            InstagramAddress = instagramAddress;
            LanguageId = languageId;
            EitaaAddress = eitaaAddress;
            Id = id;
        }
    }
}
