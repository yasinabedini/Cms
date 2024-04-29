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
        public string Text { get; set; }
        public string? MainImage { get; private set; }
        public string? SecondImage { get; private set; }
        public string? ThirdImage { get; private set; }
        public bool IsEnable { get; set; }

        public UpdateNewsCommand(long id, string title, string introduction, long languageId, long newsTypeId, string text, string? mainImage, string? secondImage, string? thirdImage, bool isEnable)
        {
            Id = id;
            Title = title;
            Introduction = introduction;
            LanguageId = languageId;
            NewsTypeId = newsTypeId;            
            Text = text;
            MainImage = mainImage;
            SecondImage = secondImage;
            ThirdImage = thirdImage;
            IsEnable = isEnable;
        }
    }
}
