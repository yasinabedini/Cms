using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.AddLink
{
    public class AddLinkCommand:ICommand
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public long LanguageId { get; set; }

        public AddLinkCommand()
        {
            
        }
        public AddLinkCommand(string title, string link, long languageId)
        {
            Title = title;
            Link = link;
            LanguageId = languageId;
        }
    }
}
