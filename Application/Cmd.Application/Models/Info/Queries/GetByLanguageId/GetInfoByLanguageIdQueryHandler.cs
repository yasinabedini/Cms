using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Info.Queries.Common;
using Cmd.Application.Models.Info.Queries.GetById;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Queries.GetByLanguageId
{
    public class GetInfoByLanguageIdQueryHandler:IQueryHandler<GetInfoByLanguageIdQuery,InfoViewModel>
    {
        private readonly IInfoRepository _repository;

        public GetInfoByLanguageIdQueryHandler(IInfoRepository repository)
        {
            _repository = repository;
        }

        public Task<InfoViewModel> Handle(GetInfoByLanguageIdQuery request, CancellationToken cancellationToken)
        {
            var info = _repository.GetList().FirstOrDefault(t => t.LanguageId == request.LanguageId);

            if (info is null) return null;
            return Task.FromResult(new InfoViewModel(info.Address, info.WorkTime, info.PhoneNumber, info.EmailAddress, info.InstagramAddress, info.LanguageId));
        }
    }
}
