using Cmd.Application.Common.Commands;
using Cmd.Application.Convertors;
using Cmd.Application.Tools;
using Cms.Domain.Models.Language.Repositories;
using Cms.Domain.Models.News.Repository;
using Ganss.Xss;

namespace Cmd.Application.Models.News.Commands.Create
{
    public class CreateNewsCommandHandler : ICommandHandler<CreateNewsCommand>
    {
        private readonly INewsRepository _repository;
        private readonly INewsTypeRepository _typeRepository;
        private readonly ILanguageRepository _languageRepository;

        public CreateNewsCommandHandler(INewsRepository repository, INewsTypeRepository typeRepository, ILanguageRepository languageRepository)
        {
            _repository = repository;
            _typeRepository = typeRepository;
            _languageRepository = languageRepository;
        }

        public Task Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            var sanitizer = new HtmlSanitizer();
            string newsContent = sanitizer.Sanitize(request.Text);

            if (_typeRepository.GetById(request.NewsTypeId) is null)
            {
                return Task.FromException(new Exception("News Type Id Is Not Available."));
            }
            if (_languageRepository.GetById(request.LanguageId) is null)
            {
                return Task.FromException(new Exception("Language Id Is Not Available."));
            }
            _repository.Add(Cms.Domain.Models.News.Entities.News.Create(request.Title, request.Introduction, newsContent, request.LanguageId, request.NewsTypeId, request.PublishDate.ToShamsi(), request.MainImage, request.SecondImage, request.ThirdImage));
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
