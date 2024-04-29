using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.Common
{
    public class NewsTypeViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public bool IsPage { get; set; }
        public bool IsEnable { get; set; }
        public int LanguageId { get; set; }
        public NewsTypeViewModel(long id, string title, string name, bool isPage, bool isEnable, int languageId)
        {
            Id = id;
            Title = title;
            Name = name;
            IsPage = isPage;
            IsEnable = isEnable;
            LanguageId = languageId;
        }
    }
}
