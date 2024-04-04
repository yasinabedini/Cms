using Cms.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Info.Entities
{
    public class Info : AggregateRoot
    {
        #region Properties
        public string Address { get; private set; }
        public string WorkTime { get; private set; }

        [Phone]
        public string PhoneNumber { get; private set; }

        [EmailAddress]
        public string EmailAddress { get; private set; }
        public string InstagramAddress { get; private set; }
        #endregion

        #region Costructors And Factories
        protected Info() { }
        private Info(string address, string workTime, string phoneNumber, string emailAddress, string instagramAddress)
        {
            Address = address;
            WorkTime = workTime;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            InstagramAddress = instagramAddress;
        }
        public static Info Create(string address, string workTime, string phoneNumber, string emailAddress, string instagramAddress)
        {
            return new Info(address, workTime, phoneNumber, emailAddress, instagramAddress);
        }
        #endregion

        #region Methods
        public void ChangeAddress(string address)
        {
            Address = address;
            Modified();
        }

        public void ChangeWorkTime(string workTime)
        {
            WorkTime = workTime;
            Modified();
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            Modified();
        }

        public void ChangeEmailAddress(string emailAddress)
        {
            EmailAddress = emailAddress;
            Modified();
        }

        public void ChangeInstagramAddress(string instagramAddress)
        {
            InstagramAddress = instagramAddress;
            Modified();
        }
        #endregion
    }
}
