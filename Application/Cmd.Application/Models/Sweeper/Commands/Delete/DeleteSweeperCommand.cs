using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Commands.Delete
{
    public class DeleteSweeperCommand:ICommand
    {
        public int Id { get; set; }
    }
}
