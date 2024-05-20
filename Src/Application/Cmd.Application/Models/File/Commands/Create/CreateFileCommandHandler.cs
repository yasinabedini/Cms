using Cmd.Application.Common.Commands;
using Cms.Domain.Models.File.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.File.Commands.Create
{
    public class CreateFileCommandHandler : ICommandHandler<CreateFileCommand>
    {
        private readonly IFileRepository _repository;

        public CreateFileCommandHandler(IFileRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            _repository.Add(new Cms.Domain.Models.File.Entities.File(request.Name, request.DisplayName,request.GalleryId, request.Length, request.Extension, request.TypeId));
            _repository.Save();
            return Task.CompletedTask;
        }
    }
}
