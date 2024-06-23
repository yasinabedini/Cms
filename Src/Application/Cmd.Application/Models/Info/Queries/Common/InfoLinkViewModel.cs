using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Queries.Common
{
    public class InfoLinkViewModel
    {
        public long Id { get; set; }
        public string Title { get; private set; }
        public string Link { get; private set; }
        public long LanguageId { get; private set; }

        public InfoLinkViewModel()
        {
            
        }
        public InfoLinkViewModel(long id,string title, string link, long languageId)
        {
            Title = title;
            Link = link;
            LanguageId = languageId;
            Id = id;
        }
    }
}
