namespace Cms.Endpoints.AdminPanel.Pages.Setting
{
    public class LinkViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public long LanguageId { get; set; }

        public LinkViewModel()
        {
            
        }
        public LinkViewModel(string title, string link, long languageId)
        {
            Title = title;
            Link = link;
            LanguageId = languageId;
        }
    }
}
