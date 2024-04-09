using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Info.Queries.Common;
using Cms.Domain.Models.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Queries.GetAll
{
    public class GetAllInfoQueryHandler : IQueryHandler<GetAllInfoQuery, PagedData<InfoViewModel>>
    {
        private readonly IInfoRepository _repository;

        public GetAllInfoQueryHandler(IInfoRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedData<InfoViewModel>> Handle(GetAllInfoQuery request, CancellationToken cancellationToken)
        {
            var infoList = _repository.GetList().Skip(request.SkipCount).Take(request.PageSize).OrderByDescending(t=>t.CreateAt).ToList();

            return Task.FromResult(new PagedData<InfoViewModel> { QueryResult = infoList.Select(t => new InfoViewModel(t.Address, t.WorkTime, t.PhoneNumber, t.EmailAddress, t.InstagramAddress, t.LanguageId)).ToList(), PageNumber = request.PageNumber, PageSize = request.PageSize });
        }
    }
}
