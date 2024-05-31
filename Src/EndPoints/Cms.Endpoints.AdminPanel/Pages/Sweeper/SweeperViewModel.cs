using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cms.Endpoints.AdminPanel.Pages.Sweeper
{
    public class SweeperViewModel
    {
        public long Id { get; set; }

        [DisplayName("عنوان")]
        [Required(ErrorMessage ="{0} را وارد کنید!")]
        public string Title { get; set; }

        [DisplayName("متن")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public string Text { get; set; }

        [DisplayName("لینک")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public string Link { get; set; }

        [DisplayName("تصویر")]        
        public string? ImageName { get; set; }

        [DisplayName("وضعیت")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public bool IsEnable { get; set; }

        [DisplayName("زبان")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public long LanguageId { get; set; }
    }
}
