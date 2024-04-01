using Cmd.Application.Common.Commands;
using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Sweeper.Queries.Common;
using Cmd.Application.Models.Sweeper.Queries.GetAll;
using Cms.Domain.Models.Sweeper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Queries.GetAllForWeb
{
    public class GetAllSweeperForWebQueryHandler : IQueryHandler<GetAllSweeperForWebQuery, PagedData<SweeperViewModel>>
    {
        private readonly ISweeperRepository _repository;

        public GetAllSweeperForWebQueryHandler(ISweeperRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedData<SweeperViewModel>> Handle(GetAllSweeperForWebQuery request, CancellationToken cancellationToken)
        {
            var sweepers = _repository.GetList().Where(t=>t.Enable).Skip(request.SkipCount).Take(request.PageSize);

            return Task.FromResult(new PagedData<SweeperViewModel>()
            {
                QueryResult = sweepers.Select(t => new SweeperViewModel(t.Title.Value, t.Text.Value, t.Link, t.ImageName, t.Enable, t.LanguageId)).ToList(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            });
        }
    }
}
