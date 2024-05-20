using Cmd.Application.Common.Queries;
using Cmd.Application.Models.File.Queries.Common;
using Cms.Domain.Models.File.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.File.Queries.GetAllFileTypes
{
    public class GetAllFileTypesQueryHandler : IQueryHandler<GetAllFileTypesQuery, List<FileTypeViewModel>>
    {
        private readonly IFileRepository _repository;

        public GetAllFileTypesQueryHandler(IFileRepository repository)
        {
            _repository = repository;
        }

        public Task<List<FileTypeViewModel>> Handle(GetAllFileTypesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetAllFileTypes().Select(t => new FileTypeViewModel { Id = (int)t.Id, Title = t.Title, ParentId = t.ParentId is null ? 0: (int)t.ParentId }).ToList());
        }
        
    }
}
