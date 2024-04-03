using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.Delete
{
    public class DeleteNewsCommand:ICommand
    {
        public int Id { get; set; }
    }
}
