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
        public string Introduction { get; set; }
        public long LanguageId { get; private set; }
        public long NewsTypeId { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Text { get; set; }
        public string? MainImage { get; set; }
        public string? SecondImage { get; set; }
        public string? ThirdImage { get; set; }

        public CreateNewsCommand(string title, string introduction, long languageId, long newsTypeId, DateTime publishDate, string text, string? mainImage, string? secondImage, string? thirdImage)
        {
            Title = title;
            Introduction = introduction;
            LanguageId = languageId;
            NewsTypeId = newsTypeId;
            PublishDate = publishDate;
            Text = text;
            MainImage = mainImage;
            SecondImage = secondImage;
            ThirdImage = thirdImage;
        }
    }
}
