namespace Cms.Endpoints.AdminPanel.Pages.Users
{
    public class SiteUserViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool IsBlocked { get; private set; }
        public DateTime? LastLoginDate { get; private set; }

        public SiteUserViewModel()
        {
            
        }
        public SiteUserViewModel(long id, string firstName, string lastName, string phoneNumber, bool phoneConfirmed, bool isBlocked, DateTime? lastLoginDate)
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
