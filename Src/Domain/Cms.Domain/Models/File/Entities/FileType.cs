using Cms.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.File.Entities
{
    public class FileType : Entity<long>
    {
        public long? ParentId { get;private set; }
        public string Title { get;private set; }
        public FileType Parent { get; set; }

        public FileType()
        {
            
        }
        public FileType(long? parentId, string title, FileType parent)
        {
            ParentId = parentId;
            Title = title;
            Parent = parent;
        }
    }
}
