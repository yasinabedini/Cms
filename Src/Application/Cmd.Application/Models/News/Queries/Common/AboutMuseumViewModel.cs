using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.Common
{
    public class AboutMuseumViewModel
    {
        public int TypeId { get; set; }
        public string TypeTitle { get; set; }
        public NewsViewModel NewsViewModel { get; set; }

        public AboutMuseumViewModel()
        {
            
        }
        public AboutMuseumViewModel(int typeId, string typeTitle, NewsViewModel newsViewModel)
        {
            TypeId = typeId;
            TypeTitle = typeTitle;
            NewsViewModel = newsViewModel;
        }
    }
}
