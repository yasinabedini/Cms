using Cmd.Application.Common.Commands;
using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.News.ValueObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.Create
{
    public class CreateNewsCommand:ICommand
    {
        public string Title { get; private set; }
        public long LanguageId { get; private set; }
        public long NewsTypeId { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string FirstParagraph { get; private set; }
        public string? SeconodParagraph { get; private set; }
        public string? ThirdParagraph { get; private set; }
        public IFormFile MainImage { get; set; }
        public IFormFile? SecondImage { get; set; }
        public IFormFile? ThirdImage { get; set; }

        public CreateNewsCommand(string title, long languageId, long newsTypeId, DateTime publishDate, string firstParagraph, string? seconodParagraph, string? thirdParagraph, IFormFile mainImage, IFormFile? secondImage, IFormFile? thirdImage)
        {
            Title = title;
            LanguageId = languageId;
            NewsTypeId = newsTypeId;
            PublishDate = publishDate;
            FirstParagraph = firstParagraph;
            SeconodParagraph = seconodParagraph;
            ThirdParagraph = thirdParagraph;
            MainImage = mainImage;
            SecondImage = secondImage;
            ThirdImage = thirdImage;
        }
    }
}
