using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Gallery.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Gallery.Queries.GetNewsGallery
{
    public class GetNewsGalleryQuery:IQuery<List<GalleryViewModel>>
    {
        public long Id { get; set; }
    }
}
