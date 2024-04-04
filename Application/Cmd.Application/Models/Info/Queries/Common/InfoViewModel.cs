using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Queries.Common
{
    public class InfoViewModel
    {
        public string Address { get; private set; }
        public string WorkTime { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }
        public string InstagramAddress { get; private set; }

        public InfoViewModel(string address, string workTime, string phoneNumber, string emailAddress, string instagramAddress)
        {
            Address = address;
            WorkTime = workTime;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            InstagramAddress = instagramAddress;
        }
    }
}
