using Cms.Domain.Common.Entities;
using Cms.Domain.Common.Rules;
using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.News.ValueObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.News.Entities
{
    public class News : AggregateRoot
    {
        #region Properties
        public Title Title { get; private set; }
        public long LanguageId { get; private set; }
        public long NewsTypeId { get; private set; }
        public string PublishDate { get; private set; }
        public Description Introduction { get;private set; }
        public Paragraph FirstParagraph { get; private set; }
        public Paragraph? SeconodParagraph { get; private set; }
        public Paragraph? ThirdParagraph { get; private set; }
        public Image MainImageName { get; private set; }
        public Image? SecondImage { get; private set; }
        public Image? ThirdImage { get; private set; }

        public Language.Entities.Language Language { get; private set; }
        public NewsType NewsType { get; private set; }
        #endregion

        #region Costructors and Factories
        protected News() { }
        private News(Title title, Description introduction, long languageId, long newsTypeId, string publishDate, Paragraph firstParagraph, Paragraph? seconodParagraph, Paragraph? thirdParagraph, Image mainImageName, Image? secondImage, Image? thirdImage)
        {
            Title = title ?? throw new Exception("Title Should Be has value");
            LanguageId = languageId;
            NewsTypeId = newsTypeId;
            PublishDate = publishDate;
            FirstParagraph = firstParagraph ?? throw new Exception("First Paragraph Should Be has value");
            SeconodParagraph = seconodParagraph;
            ThirdParagraph = thirdParagraph;
            MainImageName = mainImageName ?? throw new Exception("First Image Should Be has value");
            SecondImage = secondImage;
            ThirdImage = thirdImage;
            Introduction = introduction;
        }

        public static News Create(Title title, Description introduction, long languageId, long newsTypeId, string publishDate, Paragraph firstParagraph, Paragraph? seconodParagraph, Paragraph? thirdParagraph, Image mainImageName, Image? secondImage, Image? thirdImage)
        {
            return new News(title, introduction, languageId, newsTypeId, publishDate, firstParagraph, seconodParagraph, thirdParagraph, mainImageName, secondImage, thirdImage);
        }
        #endregion


    }
}
