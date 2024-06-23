using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.AddLink
{
    public class AddLinkCommandHandler : ICommandHandler<AddLinkCommand>
    {
        private readonly IInfoLinkRepository _repository;

        public AddLinkCommandHandler(IInfoLinkRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(AddLinkCommand request, CancellationToken cancellationToken)
        {
            _repository.Add(Cms.Domain.Models.Info.Entities.InfoLink.Create(request.Title, request.Link, request.LanguageId));
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
