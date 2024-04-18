using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Clients.AdminPanel.ViewModels
{
    public class SweeperViewModel
    {   
        public long Id { get; set; }
        public string Title { get;  set; }
        public string Text { get;  set; }
        public string Link { get;  set; }
        public string? ImageName { get;  set; }
        public bool IsEnable { get;  set; }
        public long LanguageId { get;  set; }

        public SweeperViewModel()
        {
            
        }
        public SweeperViewModel(string title, string text, string link, string imageName, bool isEnable, long languageId)
        {
            Title = title;
            Text = text;
            Link = link;
            ImageName = imageName;
            IsEnable = isEnable;
            LanguageId = languageId;
        }
    }
}
