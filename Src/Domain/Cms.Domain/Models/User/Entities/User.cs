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
        public Name? FirstName { get; private set; }
        public Name? LastName { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public bool PhoneConfirmed { get; private set; }
        public bool IsBlocked { get; private set; }
        public string VerificationCode { get; private set; }
        public DateTime? LastLoginDate { get; private set; }
        public string? Study { get; set; }
        public long? DegreeId { get; private set; }

        public Degree Degree { get; set; }
        #endregion

        #region Constructors And Factories
        protected User() { }

        public User(Name? firstName, Name? lastName, PhoneNumber phoneNumber, string email, long? degreeId, string? study)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            VerificationCode = GetNewVerificationCode();
            IsBlocked = true;
            PhoneConfirmed = false;
            Email = email;
            DegreeId = degreeId;
            Study = study;
        }
        public static User Create(Name? firstName, Name? lastName, PhoneNumber phoneNumber, string email, long? degreeId, string? study)
        {
            return new User(firstName, lastName, phoneNumber, email, degreeId, study);
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

        public void ChangeEmail(string email) { Email = email; Modified(); }


        public string GetNewVerificationCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }

        public string GetVerificationCode()
        {
            string oldCode = VerificationCode;

            VerificationCode = GetNewVerificationCode();

            return oldCode;
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

        public bool Login()
        {

            LastLoginDate = DateTime.Now;
            return true;
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
