using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.File.Commands.Delete
{
    
    public class DeleteFileCommand:ICommand
    {
        public string fileName { get; set; }
    }
}
