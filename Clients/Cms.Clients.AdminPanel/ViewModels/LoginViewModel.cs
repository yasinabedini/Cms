namespace Cms.Clients.AdminPanel.ViewModels
{
    public class LoginViewModel
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? ReturnUrl { get; set; } = "/";
        public bool RemmemberMe { get; set; }

        public LoginViewModel()
        {
            
        }
        public LoginViewModel(string phoneNumber, string password, string returnUrl = "/")
        {
            PhoneNumber = phoneNumber;
            Password = password;
            ReturnUrl = returnUrl;
        }
    }
}
