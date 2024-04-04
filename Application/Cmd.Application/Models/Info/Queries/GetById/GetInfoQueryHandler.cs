using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Info.Queries.Common;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Queries.GetById
{
    public class GetInfoQueryHandler : IQueryHandler<GetInfoByIdQuery, InfoViewModel>
    {
        private readonly IInfoRepository _repository;

        public GetInfoQueryHandler(IInfoRepository repository)
        {
            _repository = repository;
        }

        public Task<InfoViewModel> Handle(GetInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var info = _repository.GetById(request.Id);

            return Task.FromResult(new InfoViewModel(info.Address, info.WorkTime, info.PhoneNumber, info.EmailAddress, info.InstagramAddress,info.LanguageId));
        }
    }
}
