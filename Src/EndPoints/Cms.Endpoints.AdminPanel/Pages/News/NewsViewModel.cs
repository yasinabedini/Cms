using Cms.Endpoints.AdminPanel.Pages.NewsType;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cms.Endpoints.AdminPanel.Pages.News
{
    public class NewsViewModel
    {
        public long Id { get; set; }

        [DisplayName("عنوان")]
        [Required(ErrorMessage ="{0} را وارد کنید!")]
        public string Title { get; set; }

        [DisplayName("مقدمه")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public string Introduction { get; set; }

        [DisplayName("زبان")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public long LanguageId { get; set; }

        [DisplayName("نوع خبر")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public long NewsTypeId { get; set; }

        [DisplayName("تاریخ انتشار")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public string? PublishDate { get; set; }

        [DisplayName("متن")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public string Text { get; set; }

        [DisplayName("نویسنده")]        
        public string? Author { get; set; }

        [DisplayName("عکس اصلی")]        
        public string? MainImageName { get; set; }

        [DisplayName("عکس جانبی")]        
        public string? SecondImage { get; set; }

        [DisplayName("عکس جانبی")]       
        public string? ThirdImage { get; set; }

        [DisplayName("وضعیت")]        
        public bool IsEnable { get; set; }

        public NewsTypeViewModel? NewsType { get; set; }

        public NewsViewModel()
        {

        }
        public NewsViewModel(long id, string title, string introduction, long languageId, long newsTypeId, string? publishDate, string text, string mainImageName, string? secondImage, string? thirdImage, bool isEnable, string author)
        {
            Id = id;
            Title = title;
            Introduction = introduction;
            LanguageId = languageId;
            NewsTypeId = newsTypeId;
            PublishDate = publishDate;
            Text = text;
            MainImageName = mainImageName;
            SecondImage = secondImage;
            ThirdImage = thirdImage;
            IsEnable = isEnable;
            Author = author;
        }
    }
}
