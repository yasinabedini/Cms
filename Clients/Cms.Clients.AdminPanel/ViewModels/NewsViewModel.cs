namespace Cms.Clients.AdminPanel.ViewModels
{
    public class NewsViewModel
    {
        public string Title { get; private set; }
        public string Introduction { get; set; }
        public long LanguageId { get; private set; }
        public long NewsTypeId { get; private set; }
        public string PublishDate { get; private set; }
        public string Text { get; set; }
        public string MainImageName { get; private set; }
        public string? SecondImage { get; private set; }
        public string? ThirdImage { get; private set; }
        public NewsTypeViewModel NewsType { get; set; }
        public bool IsEnable { get; set; }

        public NewsViewModel(string title, string introduction, long languageId, long newsTypeId, string publishDate, string text, string mainImageName, string? secondImage, string? thirdImage, bool isEnable)
        {
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
        }
    }
}
