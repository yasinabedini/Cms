using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Sweeper.Queries.Common;
using Cms.Domain.Models.Sweeper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Queries.GetById
{
    public class GetSweeperByIdQueryHandler : IQueryHandler<GetSweeperByIdQuery, SweeperViewModel>
    {
        private readonly ISweeperRepository _repository;

        public GetSweeperByIdQueryHandler(ISweeperRepository repository)
        {
            _repository = repository;
        }

        public Task<SweeperViewModel> Handle(GetSweeperByIdQuery request, CancellationToken cancellationToken)
        {
            var sweeper = _repository.GetById(request.Id);
            return Task.FromResult(new SweeperViewModel(sweeper.Title.Value, sweeper.Text.Value, sweeper.Link, sweeper.ImageName,sweeper.IsEnable , sweeper.LanguageId));
        }
    }
}
