using Cmd.Application.Common.Commands;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public IFormFile MainImage { get; private set; }
        public IFormFile? SecondImage { get; private set; }
        public IFormFile? ThirdImage { get; private set; }

        public UpdateNewsCommand(long id, string title, string introduction, long languageId, long newsTypeId, DateTime publishDate, string firstParagraph, string? seconodParagraph, string? thirdParagraph, IFormFile mainImage, IFormFile? secondImage, IFormFile? thirdImage)
        {
            Id = id;
            Title = title;
            Introduction = introduction;
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
