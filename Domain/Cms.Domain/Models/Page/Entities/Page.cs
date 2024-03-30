using Cms.Domain.Common.Entities;
using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.Language.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Page.Entities
{
    public class Page:AggregateRoot
    {
        public Title Title { get;private set; }
        public Description Text { get; private set; }
        public string Link { get; private set; }
        public string ImageName { get; private set; }
        public bool Enable { get; private set; }
        public long LanguageId { get; private set; }

        public Language.Entities.Language Language { get; set; }

        protected Page() { }
        private Page(Title title, Description text, string link, string imageName, bool enable, long languageId)
        {
            Title = title;
            Text = text;
            Link = link;
            ImageName = imageName;
            Enable = enable;
            LanguageId = languageId;            
        }

        public static Page Create(Title title, Description text, string link, string imageName, bool enable, long languageId)
        {
            return new Page(title,text, link, imageName, enable, languageId);
        }
    }
}
