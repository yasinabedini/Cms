using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Commands.Update
{
    public class UpdateSweeperCommand:ICommand
    {
        public long Id { get; set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string Link { get; private set; }
        public string ImageName { get; private set; }
        public bool IsEnable { get; private set; }
        public long LanguageId { get; private set; }

        public UpdateSweeperCommand(long id, string title, string text, string link, string imageName, bool IsEnable, long languageId)
        {
            Id = id;
            Title = title;
            Text = text;
            Link = link;
            ImageName = imageName;
            IsEnable = IsEnable;
            LanguageId = languageId;
        }
    }
}
