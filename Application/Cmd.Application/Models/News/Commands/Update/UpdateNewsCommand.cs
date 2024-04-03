using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.Update
{
    public class UpdateNewsCommand:ICommand
    {
        public long Id { get; set; }
        public string Title { get; private set; }
        public string Introduction { get; set; }
        public long LanguageId { get; private set; }
        public long NewsTypeId { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string FirstParagraph { get; private set; }
        public string? SeconodParagraph { get; private set; }
        public string? ThirdParagraph { get; private set; }
        public string MainImageName { get; private set; }
        public string? SecondImage { get; private set; }
        public string? ThirdImage { get; private set; }

        public UpdateNewsCommand(long id, string title, long languageId, long newsTypeId, DateTime publishDate, string firstParagraph, string? seconodParagraph, string? thirdParagraph, string mainImageName, string? secondImage, string? thirdImage, string introduction)
        {
            Id = id;
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
            Introduction = introduction;
        }
    }
}
