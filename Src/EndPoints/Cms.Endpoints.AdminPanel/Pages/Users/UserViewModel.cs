using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cms.Endpoints.AdminPanel.Pages.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [DisplayName("نام")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        [StringLength(80, ErrorMessage = "حداکثر طول نام مبتواند 80 کاراکتر باشد")]
        public string Name { get; set; }

        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public string UserName { get; set; }

        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        [EmailAddress(ErrorMessage = "ایمیل خود را به درستی وارد کنید")]
        public string? Email { get; set; }

        [DisplayName("شماره موبایل")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        [Phone(ErrorMessage = "شماره موبایل خود را به درستی وارد کنید")]
        public string PhoneNumber { get; set; }

        [DisplayName("وضعیت")]
        public bool LockoutEnabled { get; set; }

        [DisplayName("نقش کاربری")]
        public List<string>? Roles { get; set; }
    }
}
