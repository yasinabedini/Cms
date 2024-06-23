using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.UpdateLink
{
    public class UpdateLinkCommand:ICommand
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public long LanguageId { get; set; }

        public UpdateLinkCommand()
        {
            
        }

        public UpdateLinkCommand(long id, string title, string link, long languageId)
        {
            Id = id;
            Title = title;
            Link = link;
            LanguageId = languageId;
        }
    }
}
