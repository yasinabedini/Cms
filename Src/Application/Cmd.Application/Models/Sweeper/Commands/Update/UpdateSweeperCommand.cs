using Cmd.Application.Common.Commands;
using Microsoft.AspNetCore.Http;
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
        public string Title { get;  set; }
        public string Text { get;  set; }
        public string Link { get;  set; }
        public string ImageName { get; set; }
        public bool IsEnable { get;  set; }
        public long LanguageId { get;  set; }


        public UpdateSweeperCommand()
        {
            
        }
        public UpdateSweeperCommand(long id, string title, string text, string link, string image, bool isEnable, long languageId)
        {
            Id = id;
            Title = title;
            Text = text;
            Link = link;
            ImageName = image;
            IsEnable = isEnable;
            LanguageId = languageId;
        }
    }
}
