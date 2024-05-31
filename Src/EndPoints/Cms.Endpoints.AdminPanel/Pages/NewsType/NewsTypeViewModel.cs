using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Cms.Endpoints.AdminPanel.Pages.NewsType
{
    public class NewsTypeViewModel
    {
        public long Id { get; set; }

        [DisplayName("عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public string Title { get; set; }

        [DisplayName("نام")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public string Name { get; set; }
                
        public bool IsPage { get; set; }
        public bool IsEnable { get; set; }

        [DisplayName("زبان")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public int LanguageId { get; set; }

        public NewsTypeViewModel()
        {

        }
        public NewsTypeViewModel(string title, string name, bool isPage, bool isEnable, int languageId)
        {
            Title = title;
            Name = name;
            IsPage = isPage;
            IsEnable = isEnable;
            LanguageId = languageId;
        }
    }
}
