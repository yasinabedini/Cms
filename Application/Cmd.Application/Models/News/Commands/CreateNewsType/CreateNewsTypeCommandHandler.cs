using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Language.Entities;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.CreateNewsType
{
    public class CreateNewsTypeCommandHandler : ICommandHandler<CreateNewsTypeCommand>
    {
        private readonly INewsTypeRepository _repository;

        public CreateNewsTypeCommandHandler(INewsTypeRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(CreateNewsTypeCommand request, CancellationToken cancellationToken)
        {
            _repository.Add(Cms.Domain.Models.News.Entities.NewsType.Create(request.Title, request.Name, request.LanguageId,request.IsPage));
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
