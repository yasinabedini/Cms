using Cms.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Info.Entities
{
    public class InfoLink : Entity
    {
        public string Title { get;private set; }
        public string Link { get;private set; }
        public long LanguageId { get;private set; }


        public Language.Entities.Language Language { get; set; }

        public InfoLink()
        {
            
        }
        public InfoLink(string title, string link, long languageId)
        {
            Title = title;
            Link = link;
            LanguageId = languageId;
        }

        public static InfoLink Create(string title, string link, long languageId)
        {
            return new InfoLink(title, link, languageId);
        }


        public void ChangeTitle(string title)
        {
            Title = title;
            Modified();
        }
        public void ChangeLink(string link)
        {
            Link = link;
            Modified();
        }
        public void ChangeLanguageId(long languageId)
        {
            LanguageId = languageId;
            Modified();
        }
    }
}
