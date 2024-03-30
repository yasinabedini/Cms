using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Language.Commands.Delete
{
    public class DeleteLanguageCommand:ICommand
    {
        public int Id { get; set; }
    }
}
