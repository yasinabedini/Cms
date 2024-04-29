using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Language.Entities;
using Cms.Domain.Models.Language.Repositories;
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

        public UpdateNewsTypeCommandHandler(INewsTypeRepository repository, INewsTypeRepository typeRepository, ILanguageRepository languageRepository)
        {
            _repository = repository;            
        }

        public Task Handle(UpdateNewsTypeCommand request, CancellationToken cancellationToken)
        {
            var newsType = _repository.GetById(request.Id);

            newsType.ChangeName(request.Name);
            newsType.ChangeTitle(request.Title);
            newsType.ChangeIsPage(request.IsPage);
            newsType.ChangeLanguageId(request.LanguageId);
            newsType.ChangeIsEnable(request.IsEnable);


            _repository.Update(newsType);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
