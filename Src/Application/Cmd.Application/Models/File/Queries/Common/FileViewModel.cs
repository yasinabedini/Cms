using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.File.Queries.Common
{
    public class FileViewModel
    {
        public string Name { get;  set; }
        public long GalleryId { get;  set; }
        public string Type { get;  set; }
        public int Length { get;  set; }
        public string Extension { get; set; }
        public FileViewModel()
        {
            
        }

        public FileViewModel(string name, long galleryId, string type, int length, string extension)
        {
            Name = name;
            GalleryId = galleryId;
            Type = type;
            Length = length;
            Extension = extension;
        }
    }
}
