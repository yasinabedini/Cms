using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Language.Commands.Create
{
    public class CreateLanguageCommand:ICommand
    {
        public string Title { get; private set; }
        public string Name { get; private set; }
        public bool Rtl { get; private set; }
        public string Region { get; private set; }

        public CreateLanguageCommand(string title, string name, bool rtl, string region)
        {
            Title = title;
            Name = name;
            Rtl = rtl;
            Region = region;
        }
    }
}
