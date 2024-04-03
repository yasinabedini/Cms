using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.CreateNewsType
{
    public class CreateNewsTypeCommand:ICommand
    {
        public string Title { get; set; }
        public string Name { get; set; }

        public CreateNewsTypeCommand(string title, string name)
        {
            Title = title;
            Name = name;
        }
    }
}
