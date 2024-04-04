using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Info.Queries.Common;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Queries.Get
{
    public class GetInfoQueryHandler : IQueryHandler<GetInfoQuery, InfoViewModel>
    {
        private readonly IInfoRepository _repository;

        public GetInfoQueryHandler(IInfoRepository repository)
        {
            _repository = repository;
        }

        public Task<InfoViewModel> Handle(GetInfoQuery request, CancellationToken cancellationToken)
        {
            var info = _repository.GetList().First();

            return Task.FromResult(new InfoViewModel(info.Address, info.WorkTime, info.PhoneNumber, info.EmailAddress, info.InstagramAddress));
        }
    }
}
