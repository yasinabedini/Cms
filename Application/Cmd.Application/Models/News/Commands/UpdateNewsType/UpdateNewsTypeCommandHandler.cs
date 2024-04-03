using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Language.Entities;
using Cms.Domain.Models.News.Entities;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.UpdateNewsType
{
    public class UpdateNewsTypeCommandHandler : ICommandHandler<UpdateNewsTypeCommand>
    {
        private readonly INewsTypeRepository _repository;

        public UpdateNewsTypeCommandHandler(INewsTypeRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(UpdateNewsTypeCommand request, CancellationToken cancellationToken)
        {
            var newsType = NewsType.Create(request.Title, request.Name, request.LanguageId);
            newsType.SetId(request.Id);

            _repository.Update(newsType);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
