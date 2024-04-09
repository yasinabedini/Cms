using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.Delete
{
    public class DeleteInfoCommand:ICommand
    {
        public long Id { get; set; }
    }
}
