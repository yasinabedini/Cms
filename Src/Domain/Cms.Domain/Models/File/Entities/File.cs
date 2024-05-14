using Cms.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.File.Entities
{
    public class File : AggregateRoot
    {
        public string Name { get; private set; }
        public long GalleryId { get; private set; }
        public string Type { get; private set; }
        public int Length { get; private set; }
        public string Extension { get;private set; }

        public Gallery.Entities.Gallery Gallery { get; set; }

        #region Constructors and factories
        public File() { }

        public File(string name, long galleryId, string type, int length, string extension)
        {
            Name = name;
            GalleryId = galleryId;
            Type = type;
            Length = length;
            Extension = extension;
        }

        public static File Create()
        {
            return new File();
        }

        public static File Create(string name, long galleryId, string type, int length, string extension)
        {
            return new File(name, galleryId, type, length,extension);
        }
        #endregion
    }
}
