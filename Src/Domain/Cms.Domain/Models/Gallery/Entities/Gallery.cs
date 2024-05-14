using Cms.Domain.Common.Entities;
using Cms.Domain.Models.Gallery.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Gallery.Entities
{
    public class Gallery : AggregateRoot
    {
        public string? Title { get; private set; }
        public GalleryType Type { get; private set; }
        public long NewsId { get; private set; }

        public News.Entities.News News { get; set; }

        #region constructors and factories
        public Gallery() { }
        public Gallery(string? title, GalleryType type, long newsId)
        {
            Title = title;
            Type = type;
            NewsId = newsId;
        }
        public static Gallery Create()
        {
            return new Gallery();
        }
        public static Gallery Create(string? title, GalleryType type, long newsId)
        {
            return new Gallery(title, type, newsId);
        } 
        #endregion

        public void ChangeTitle(string title)
        {
            Title = title;
            Modified();
        }
    }
}
