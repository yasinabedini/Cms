using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Queries.Common
{
    public class SweeperViewModel
    {
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string Link { get; private set; }
        public string ImageName { get; private set; }
        public bool IsEnable { get; private set; }
        public long LanguageId { get; private set; }

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
