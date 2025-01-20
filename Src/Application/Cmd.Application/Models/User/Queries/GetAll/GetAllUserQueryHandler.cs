using Cmd.Application.Common.Queries;
using Cmd.Application.Models.User.Queries.Common;
using Cms.Domain.Models.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.User.Queries.GetAll
{
    public class GetAllUserQueryHandler : IQueryHandler<GetAllUserQuery, PagedData<UserViewModel>>
    {
        private readonly IUserRepository _repository;

        public GetAllUserQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<PagedData<UserViewModel>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var user = _repository.GetList();

            //var userList = user.Skip(request.SkipCount).Take(request.PageSize).Select(t => new UserViewModel(t.Id, t.FirstName.Value, t.LastName.Value, t.PhoneNumber.Value, t.PhoneConfirmed,t.IsBlocked,t.LastLoginDate)).ToList();

            return Task.FromResult(new PagedData<UserViewModel>
            {
                QueryResult = user.Select(t => new UserViewModel(t.Id, t.FirstName.Value, t.LastName.Value, t.PhoneNumber.Value, t.PhoneConfirmed, t.IsBlocked, t.LastLoginDate)).ToList(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = user.Count,
            });
        }
    }
}
