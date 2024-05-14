using Cms.Domain.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.File.Repositories
{
    public interface IFileRepository : IRepository<File.Entities.File>
    {
        List<Entities.File> GetGalleryFiles(long galleryId);
    }
}
