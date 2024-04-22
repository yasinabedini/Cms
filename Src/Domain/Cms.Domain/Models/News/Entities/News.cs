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
        public string Text { get; set; }        
        public Image MainImageName { get; private set; }
        public Image? SecondImage { get; private set; }
        public Image? ThirdImage { get; private set; }

        public Language.Entities.Language Language { get; private set; }
        public NewsType NewsType { get; private set; }
        #endregion

        #region Costructors and Factories
        protected News() { }

        public News(Title title, Description introduction, string text, long languageId, long newsTypeId, string publishDate, Image mainImageName, Image? secondImage, Image? thirdImage)
        {
            Title = title;
            LanguageId = languageId;
            NewsTypeId = newsTypeId;
            PublishDate = publishDate;
            Introduction = introduction;
            Text = text;            
            MainImageName = mainImageName;
            SecondImage = secondImage;
            ThirdImage = thirdImage;
        }

        public static News Create(Title title, Description introduction, string text, long languageId, long newsTypeId, string publishDate, Image mainImageName, Image? secondImage, Image? thirdImage)
        {
            return new News(title, introduction, text, languageId, newsTypeId, publishDate,mainImageName,secondImage, thirdImage);
        }
        #endregion

        public void ChangeTitle(string title)
        {
            Title = title;
            Modified();
        }
        public void ChangeIntroduction(string introduction)
        {
            Introduction = introduction;
            Modified();
        }
        public void ChangePublishDate(string publishDate)
        {
            PublishDate = publishDate;
            Modified();
        }
        public void ChangeText(string text)
        {
            Text = text;
            Modified();
        }
        public void ChangeMainImage(string mainImage)
        {
            MainImageName = mainImage;
            Modified();
        }
        public void ChangeSecondImage(string secondImage)
        {
            SecondImage = secondImage;
            Modified();
        }
        public void ChangeThirdImage(string thirdImage)
        {
            ThirdImage = thirdImage;
            Modified();
        }
        public void ChangeLanguageId(long languageId )
        {
            LanguageId = languageId;
            Modified();
        }
        public void ChangeNewsTypeId(long newsTypeId )
        {
            NewsTypeId = newsTypeId;
            Modified();
        }
    }
}
