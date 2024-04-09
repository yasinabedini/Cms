using Cmd.Application.Common.Commands;
using Cms.Domain.Common.ValueObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Commands.Create
{
    public class CreateSweeperCommand : ICommand
    {
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string Link { get; private set; }
        public IFormFile Image { get; set; }
        public long LanguageId { get; private set; }

        public CreateSweeperCommand(string title, string text, string link, IFormFile image, long languageId)
        {
            Title = title;
            Text = text;
            Link = link;
            Image = image;
            LanguageId = languageId;
        }
    }
}
