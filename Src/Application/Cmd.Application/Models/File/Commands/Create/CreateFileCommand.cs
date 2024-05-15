using Cmd.Application.Common.Commands;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.File.Commands.Create
{
    public class CreateFileCommand : ICommand
    {
        public IFormFile File { get; set; }
        public long TypeId { get; set; }
    }
}
