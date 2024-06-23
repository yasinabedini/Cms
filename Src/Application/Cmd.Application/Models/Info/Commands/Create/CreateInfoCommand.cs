using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.Create
{
    public class CreateInfoCommand:ICommand
    {
        public string Address { get; private set; }
        public string WorkTime { get; private set; }        
        public string PhoneNumber { get; private set; }        
        public string EmailAddress { get; private set; }
        public string InstagramAddress { get; private set; }
        public long LanguageId { get; set; }
        public string EitaaAddress { get; set; }

        public CreateInfoCommand(string address, string workTime, string phoneNumber, string emailAddress, string instagramAddress, long languageId, string eitaaAddress)
        {
            Address = address;
            WorkTime = workTime;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            InstagramAddress = instagramAddress;
            LanguageId = languageId;
            EitaaAddress = eitaaAddress;
        }
    }
}
