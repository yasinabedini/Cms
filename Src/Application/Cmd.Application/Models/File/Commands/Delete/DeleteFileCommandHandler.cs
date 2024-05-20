using Cmd.Application.Common.Commands;
using Cms.Domain.Models.File.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.File.Commands.Delete
{
    public class DeleteFileCommandHandler : ICommandHandler<DeleteFileCommand>
    {
        private readonly IFileRepository _repository;

        public DeleteFileCommandHandler(IFileRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = _repository.GetFileByName(request.fileName);
            _repository.Delete(file.Id);
            _repository.Save();

            return Task.CompletedTask;
        }
    }

}
