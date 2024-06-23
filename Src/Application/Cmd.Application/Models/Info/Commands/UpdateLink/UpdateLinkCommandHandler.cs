using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.UpdateLink
{
    public class UpdateLinkCommandHandler : ICommandHandler<UpdateLinkCommand>
    {
        private readonly IInfoLinkRepository _repository;

        public UpdateLinkCommandHandler(IInfoLinkRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(UpdateLinkCommand request, CancellationToken cancellationToken)
        {
            var model = _repository.GetById(request.Id);
            model.ChangeTitle(request.Title);
            model.ChangeLink(request.Link);
            model.ChangeLanguageId(request.LanguageId);
            _repository.Update(model);
            _repository.Save();
            return Task.CompletedTask;
        }
    }
}
