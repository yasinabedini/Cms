using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.File.Commands.Create
{
    public class CreateFileCommandHandler : ICommandHandler<CreateFileCommand>
    {
        public Task Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
