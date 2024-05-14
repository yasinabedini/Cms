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
        public GalleryRepository(CmsDbContext context) : base(context)
        {
        }
    }
}
