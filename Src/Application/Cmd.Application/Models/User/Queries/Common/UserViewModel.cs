using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Queries.Common
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool IsBlocked { get; private set; }
        public DateTime? LastLoginDate { get; private set; }

        public UserViewModel()
        {

        }
        public UserViewModel(long id, string firstName, string lastName, string phoneNumber, bool phoneConfirmed, bool isBlocked, DateTime? lastLoginDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            PhoneConfirmed = phoneConfirmed;
            IsBlocked = isBlocked;
            LastLoginDate = lastLoginDate;
        }
    }
}
