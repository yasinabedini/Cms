namespace Cms.Clients.AdminPanel.ViewModels
{
	public class UpdateUserViewModel
	{
        public string Id { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }

        public string? Password { get; set; }
        public string? Re_Password { get; set; }
    }
}
