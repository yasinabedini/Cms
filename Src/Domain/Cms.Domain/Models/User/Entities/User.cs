using Cms.Domain.Common.Entities;
using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.User.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.User.Entities
{
    public class User : AggregateRoot
    {
        #region Properties
        public Name? FirstName { get;private set; }
        public Name? LastName { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public string Password { get; private set; }
        public bool PhoneConfirmed { get; private set; }
        public bool IsBlocked { get; private set; }
        public string VerificationCode { get; private set; }
        public DateTime? LastLoginDate { get; private set; }
        #endregion

        #region Constructors And Factories
        protected User() { }

        public User(Name? firstName, Name? lastName, PhoneNumber phoneNumber, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            VerificationCode = GetNewVerificationCode();
            IsBlocked = true;
            PhoneConfirmed = false;
            Password = password;
        }
        public static User Create(Name? firstName, Name? lastName, PhoneNumber phoneNumber, string password)
        {
            return new User(firstName, lastName, phoneNumber, password);
        }
        #endregion

        #region Methods
        public void ChangeFirstName(string firstName)
        {
            FirstName = firstName;
            Modified();
        }
        public void ChangeLastName(string lastName)
        {
            LastName = lastName;
            Modified();
        }
        public void ChangePassword(string hashPassword)
        {
            Password = hashPassword;
            Modified();
        }
        public string GetNewVerificationCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();            
        }
        public bool CheckVerificationCode(string code)
        {
            if (code == VerificationCode)
            {
                VerificationCode = GetNewVerificationCode();
                Modified();
                return true;
            }

            else return false;
        }
        public bool Login(string password)
        {
            if (password == Password)
            {
                LastLoginDate = DateTime.Now;
                return true;
            }
            else return false;
        }
        public void ConfirmPhoneNumber()
        {
            PhoneConfirmed = true;
            IsBlocked = false;
            Modified();
        }
        #endregion
    }
}
