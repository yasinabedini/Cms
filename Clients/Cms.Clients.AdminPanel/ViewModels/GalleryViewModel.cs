using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Clients.AdminPanel.ViewModels
{
    public class GalleryViewModel
    {
        public long Id { get; set; }    
        public string? Title { get; set; }
        public bool Type { get; set; }
        public long? NewsId { get; set; }
        public List<FileViewModel> Files { get; set; }

        public GalleryViewModel()
        {
        }
        public GalleryViewModel(string? title, bool type, long? newsId, long id)
        {
            Title = title;
            Type = type;
            NewsId = newsId;
            Id = id;
        }
    }
}
