using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.News.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.Common
{
    public class NewsViewModel
    {
        public long Id { get; set; }
        public string Title { get; private set; }
        public string Introduction { get; set; }
        public long LanguageId { get; private set; }
        public long NewsTypeId { get; private set; }
        public string PublishDate { get; private set; }
        public string  Text { get; set; }
        public string Author { get; set; }
        public string MainImageName { get; private set; }
        public string? SecondImage { get; private set; }
        public string? ThirdImage { get; private set; }
        public NewsTypeViewModel NewsType { get; set; }
        public bool IsEnable { get; set; }

        public NewsViewModel(long id, string title, string introduction, long languageId, long newsTypeId, string publishDate, string text, string mainImageName, string? secondImage, string? thirdImage, bool isEnable, string author)
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
