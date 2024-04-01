using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Sweeper.Queries.Common;
using Cmd.Application.Models.Sweeper.Queries.GetAll;
using Cms.Domain.Models.Sweeper.Repositories;

namespace Cmd.Application.Models.Sweeper.Queries;

public class GetAllSweeperQueryHandler : IQueryHandler<GetAllSweeperQuery, PagedData<SweeperViewModel>>
{
    private readonly ISweeperRepository _repository;

    public GetAllSweeperQueryHandler(ISweeperRepository repository)
    {
        _repository = repository;
    }

    public Task<PagedData<SweeperViewModel>> Handle(GetAllSweeperQuery request, CancellationToken cancellationToken)
    {
        var sweepers = _repository.GetList().Skip(request.SkipCount).Take(request.PageSize);

        return Task.FromResult(new PagedData<SweeperViewModel>()
        {
            QueryResult = sweepers.Select(t => new SweeperViewModel(t.Title.Value, t.Text.Value, t.Link, t.ImageName, t.Enable, t.LanguageId)).ToList(),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
        });
    }
}
