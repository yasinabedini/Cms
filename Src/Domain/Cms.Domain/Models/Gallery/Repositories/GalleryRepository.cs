using Cms.Domain.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Gallery.Repositories
{
    public interface IGalleryRepository : IRepository<Entities.Gallery>
    {
        List<Entities.Gallery> GetNewsGalleries(long newsId);
    }
}
