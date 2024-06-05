using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Endpoints.AdminPanel.Pages.News
{
    public class FileViewModel
    {
        public string Name { get; set; }
        public long GalleryId { get; set; }
        public int Length { get; set; }
        public string DisplayName { get; set; }
        public string Extension { get; set; }
        public long TypeId { get; set; }
        public FileViewModel()
        {

        }

        public FileViewModel(string name, long galleryId, int length, string extension, long typeId, string displayName)
        {
            Name = name;
            GalleryId = galleryId;
            Length = length;
            Extension = extension;
            TypeId = typeId;
            DisplayName = displayName;
        }
    }
}
