using Cmd.Application.Models.File.Queries.Common;
using Cms.Domain.Models.Gallery.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Gallery.Queries.Common
{
    public class GalleryViewModel
    {
        public string? Title { get;  set; }
        public bool Type { get;  set; }
        public long? NewsId { get;  set; }
        public List<FileViewModel> Files { get; set; }

        public GalleryViewModel()
        {            
        }
        public GalleryViewModel(string? title, bool type, long? newsId)
        {
            Title = title;
            Type = type;
            NewsId = newsId;
        }
    }
}
