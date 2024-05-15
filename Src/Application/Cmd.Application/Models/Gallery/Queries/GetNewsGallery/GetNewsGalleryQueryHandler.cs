using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Gallery.Queries.Common;
using Cms.Domain.Models.Gallery.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Gallery.Queries.GetNewsGallery
{
    public class GetNewsGalleryQueryHandler : IQueryHandler<GetNewsGalleryQuery, List<GalleryViewModel>>
    {
        private readonly IGalleryRepository _galleryRepository;

        public GetNewsGalleryQueryHandler(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public Task<List<GalleryViewModel>> Handle(GetNewsGalleryQuery request, CancellationToken cancellationToken)
        {
            var galleries = _galleryRepository.GetNewsGalleries(request.Id);
            return Task.FromResult(galleries.Select(t => new GalleryViewModel
            {
                NewsId = t.NewsId,
                Title = t.Title,
                Type = t.Type,
                Files = t.Files.Select(f => new File.Queries.Common.FileViewModel
                {
                    DisplayName = f.DisplayName,
                    Extension = f.Extension,
                    GalleryId = f.GalleryId,
                    Length = f.Length,
                    Name = f.Name,
                    TypeId = f.TypeId
                }).ToList()
            }).ToList());
        }
    }
}
