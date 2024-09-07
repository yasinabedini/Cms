using Cmd.Application.Common.Queries;
using Cmd.Application.Models.User.Queries.Common;
using Cmd.Application.Models.User.Queries.GetAll;
using Cms.Domain.Common.Repositories;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Queries.GetAllDegree
{
    public class GetAllDegreeQueryHandler : IQueryHandler<GetAllDegreeQuery, PagedData<DegreeViewModel>>
    {
        private readonly IUserRepository _repository;

        public GetAllDegreeQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedData<DegreeViewModel>> Handle(GetAllDegreeQuery request, CancellationToken cancellationToken)
        {
            var user = _repository.GetAllDegree();


            return Task.FromResult(new PagedData<DegreeViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = user.Count,
                QueryResult = user.Select(t => new DegreeViewModel { Id = t.Id, Title = t.Title }).ToList()
            });
        }
    }
}
