using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.Update
{
    public class UpdateInfoCommand : ICommand
    {
        public long Id { get; set; }
        public string Address { get; private set; }
        public string WorkTime { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }
        public string InstagramAddress { get; private set; }
        public long LanguageId { get; set; }
        public bool IsEnable { get; set; }

        public UpdateInfoCommand(long id, string address, string workTime, string phoneNumber, string emailAddress, string instagramAddress, long languageId, bool isEnable)
        {
            Id = id;
            Address = address;
            WorkTime = workTime;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            InstagramAddress = instagramAddress;
            LanguageId = languageId;
            IsEnable = isEnable;
        }
    }
}
