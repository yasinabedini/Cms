using Cmd.Application.Common.Commands;
using Cms.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Commands.Create
{
    public class CreateSweeperCommand:ICommand
    {
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string Link { get; private set; }
        public string ImageName { get; private set; }
        public bool Enable { get; private set; }
        public long LanguageId { get; private set; }

        public CreateSweeperCommand(string title, string text, string link, string imageName, bool enable, long languageId)
        {
            Title = title;
            Text = text;
            Link = link;
            ImageName = imageName;
            Enable = enable;
            LanguageId = languageId;
        }
    }
}
