using Cmd.Application.Common.Commands;
using Cmd.Application.Convertors;
using Cmd.Application.Tools;
using Cms.Domain.Models.Language.Repositories;
using Cms.Domain.Models.News.Repository;
using Ganss.Xss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.Update
{
    public class UpdateNewsCommandHandler : ICommandHandler<UpdateNewsCommand>
    {
        private readonly INewsRepository _repository;
        private readonly INewsTypeRepository _typeRepository;
        private readonly ILanguageRepository _languageRepository;

        public UpdateNewsCommandHandler(INewsRepository repository, ILanguageRepository languageRepository, INewsTypeRepository typeRepository)
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _typeRepository = typeRepository;
        }

        public async Task Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {            
            if (_languageRepository.GetById(request.LanguageId) is null)
            {
                await Task.FromException(new Exception("Language id is not available."));                
            }
            if (_typeRepository.GetById(request.NewsTypeId) is null)
            {
                await Task.FromException(new Exception("News Type Id Is Not Available."));
            }            

            var sanitizer = new HtmlSanitizer();
            string newsContent = sanitizer.Sanitize(request.Text);

            var news = _repository.GetById(request.Id);

            news.ChangeText(request.Text);
            news.ChangeTitle(request.Title);
            news.ChangeIntroduction(request.Introduction);
            news.ChangeMainImage(request.MainImage);
            news.ChangeSecondImage(request.SecondImage);
            news.ChangeThirdImage(request.ThirdImage);            
            news.ChangeLanguageId(request.LanguageId);
            news.ChangeNewsTypeId(request.NewsTypeId);
            news.ChangeIsEnable(request.IsEnable);
            news.ChangeThumbNailImage(request.MainImage);
            
            _repository.Update(news);            
            _repository.Save();
            

            await Task.CompletedTask;
        }
    }
}
