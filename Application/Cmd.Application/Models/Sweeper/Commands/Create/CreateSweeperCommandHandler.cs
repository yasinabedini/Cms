using Cmd.Application.Common.Commands;
using Cmd.Application.Tools;
using Cms.Domain.Models.Sweeper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Commands.Create
{
    public class CreateSweeperCommandHandler : ICommandHandler<CreateSweeperCommand>
    {
        private readonly ISweeperRepository _repository;

        public CreateSweeperCommandHandler(ISweeperRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(CreateSweeperCommand request, CancellationToken cancellationToken)
        {
            string imageName = Guid.NewGuid().ToString()+Path.GetDirectoryName(request.Image.FileName);

            FileTools.SaveImage(request.Image, imageName, "Sweeper", false);

            _repository.Add(Cms.Domain.Models.Sweeper.Entities.Sweeper.Create(request.Title, request.Text, request.Link, imageName, request.LanguageId));
            _repository.Save();

           return Task.CompletedTask;
        }
    }
}
