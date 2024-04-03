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
        public string Title { get; private set; }
        public long LanguageId { get; private set; }
        public long NewsTypeId { get; private set; }
        public string PublishDate { get; private set; }
        public string FirstParagraph { get; private set; }
        public string? SeconodParagraph { get; private set; }
        public string? ThirdParagraph { get; private set; }
        public string MainImageName { get; private set; }
        public string? SecondImage { get; private set; }
        public string? ThirdImage { get; private set; }

        public NewsViewModel(string title, long languageId, long newsTypeId, string publishDate, string firstParagraph, string? seconodParagraph, string? thirdParagraph, string mainImageName, string? secondImage, string? thirdImage)
        {
            Title = title;
            LanguageId = languageId;
            NewsTypeId = newsTypeId;
            PublishDate = publishDate;
            FirstParagraph = firstParagraph;
            SeconodParagraph = seconodParagraph;
            ThirdParagraph = thirdParagraph;
            MainImageName = mainImageName;
            SecondImage = secondImage;
            ThirdImage = thirdImage;
        }
    }
}
