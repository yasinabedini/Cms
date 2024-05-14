using Cmd.Application.Common.Queries;
using Cmd.Application.Models.File.Queries.Common;
using Cms.Domain.Models.File.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.File.Queries.GetById
{
    public class GetFileByIdQueryHandler : IQueryHandler<GetFileByIdQuery, FileViewModel>
    {
        private readonly IFileRepository _fileRepository;

        public GetFileByIdQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public Task<FileViewModel> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
        {
            var model = _fileRepository.GetById(request.Id);
            return Task.FromResult(new FileViewModel(model.Name, model.GalleryId, model.Type, model.Length,model.Extension));
        }
    }
}
