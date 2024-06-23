using System.ComponentModel.DataAnnotations;

namespace Cms.Endpoints.AdminPanel.Pages.Setting
{
    public class InfoViewModel
    {
        public long Id { get; set; }
        public string Address { get;  set; }
        public string WorkTime { get;  set; }

        public string PhoneNumber { get;  set; }
        
        public string EmailAddress { get;  set; }
        public string InstagramAddress { get;  set; }
        public string EitaaAddress { get;  set; }
        public long LanguageId { get; set; }

        public InfoViewModel()
        {
            
        }
        public InfoViewModel(long id, string address, string workTime, string phoneNumber, string emailAddress, string instagramAddress, string eitaaAddress, long languageId)
        {
            Id = id;
            Address = address;
            WorkTime = workTime;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            InstagramAddress = instagramAddress;
            EitaaAddress = eitaaAddress;
            LanguageId = languageId;
        }
    }
}
