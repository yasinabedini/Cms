using Cmd.Application.Models.Gallery.Queries.Common;
using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.News.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.Common
{
    public class NewsViewModel
    {
        public long Id { get; set; }
        public string Title { get;  set; }
        public string Introduction { get; set; }
        public long LanguageId { get;  set; }
        public long NewsTypeId { get;  set; }
        public string PublishDate { get;  set; }
        public string  Text { get; set; }
        public string Author { get; set; }
        public string MainImageName { get;  set; }
        public string? SecondImage { get;  set; }
        public string? ThirdImage { get;  set; }
        public NewsTypeViewModel NewsType { get; set; }
        public List<GalleryViewModel> Galleries { get; set; }
        public bool IsEnable { get; set; }

        public NewsViewModel()
        {
            
        }
        public NewsViewModel(long id, string title, string introduction, long languageId, long newsTypeId, string publishDate, string text, string mainImageName, string? secondImage, string? thirdImage, bool isEnable, string author)
        {
            Id = id;
            Title = title;
            Introduction = introduction;
            LanguageId = languageId;
            NewsTypeId = newsTypeId;
            PublishDate = publishDate;
            Text = text;
            MainImageName = mainImageName;
            SecondImage = secondImage;
            ThirdImage = thirdImage;
            IsEnable = isEnable;
            Author = author;
        }
    }
}
