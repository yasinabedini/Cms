using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.Create
{
    public class CreateInfoCommandHandler : ICommandHandler<CreateInfoCommand>
    {
        private readonly IInfoRepository _repository;

        public CreateInfoCommandHandler(IInfoRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(CreateInfoCommand request, CancellationToken cancellationToken)
        {
            _repository.Add(Cms.Domain.Models.Info.Entities.Info.Create(request.Address, request.WorkTime, request.PhoneNumber, request.EmailAddress, request.InstagramAddress, request.LanguageId));
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
