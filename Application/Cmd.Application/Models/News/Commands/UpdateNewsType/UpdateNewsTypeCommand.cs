using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.UpdateNewsType
{
    public class UpdateNewsTypeCommand:ICommand
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public long LanguageId { get; set; }
        public UpdateNewsTypeCommand(long id, string title, string name, long languageId)
        {
            Id = id;
            Title = title;
            Name = name;
            LanguageId = languageId;
        }
    }
}
