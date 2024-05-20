using Cms.Domain.Common.Entities;
using Cms.Domain.Common.Repositories;
using Cms.Domain.Models.File.Entities;
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
        List<FileType> GetAllFileTypes();
        Entities.File GetFileByName(string name);
    }
}
