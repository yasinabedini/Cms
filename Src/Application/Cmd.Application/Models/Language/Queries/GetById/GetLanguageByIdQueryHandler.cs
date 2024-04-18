using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Language.Queries.Common;
using Cms.Domain.Models.Language.Repositories;

namespace Cmd.Application.Models.Language.Queries.GetById
{
    public class GetLanguageByIdQueryHandler : IQueryHandler<GetLanguageByIdQuery, LanguageViewModel>
    {
        private readonly ILanguageRepository _repository;

        public GetLanguageByIdQueryHandler(ILanguageRepository repository)
        {
            _repository = repository;
        }

        public Task<LanguageViewModel> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
        {            
            var language = _repository.GetById(request.Id);
            return Task.FromResult(new LanguageViewModel(language.Id, language.Title, language.Name, language.Rtl, language.Region,language.IsEnable));
        }
    }
}
