using Cms.Domain.Models.Gallery.Repositories;
using Cms.Infra.Common.Repository;
using Cms.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Gallery.Repositories
{
    public class GalleryRepository : BaseRepository<Domain.Models.Gallery.Entities.Gallery>, IGalleryRepository
    {
        private readonly CmsDbContext _context;
        public GalleryRepository(CmsDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Domain.Models.Gallery.Entities.Gallery> GetNewsGalleries(long newsId)
        {
            return _context.Galleries.Where(t => t.NewsId == newsId).ToList();
        }
    }
}
