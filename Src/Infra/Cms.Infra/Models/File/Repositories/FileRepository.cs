using Cms.Domain.Models.File.Repositories;
using Cms.Infra.Common.Repository;
using Cms.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.File.Repositories
{
    public class FileRepository : BaseRepository<Domain.Models.File.Entities.File>, IFileRepository
    {
        private readonly CmsDbContext _context;
        public FileRepository(CmsDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Domain.Models.File.Entities.File> GetGalleryFiles(long galleryId)
        {
            return _context.Files.Where(t => t.GalleryId == galleryId).ToList();
        }
    }
}
