using Cms.Domain.Common.Entities;
using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.Language.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.News.Entities
{
    public class NewsType : Entity
    {
        #region Properties
        public Title Title { get; private set; }
        public Title Name { get; private set; }
        public long LanguageId { get; set; }
        public bool IsPage { get; set; }


        #endregion

        #region Constructors and Factories
        protected NewsType() { }
        private NewsType(Title title, Title name, long languageId, bool isPage)
        {
            Title = title;
            Name = name;
            LanguageId = languageId;
            IsPage = isPage;
        }

        public static NewsType Create(Title title, Title name,long languageId,bool isPage)
        {
            return new NewsType(title, name, languageId,isPage);
        } 
        #endregion

        public void ChangeTitle(string title)
        {
            Title = title;
            Modified();
        }

        public void ChangeName(string name)
        {
            Name = name;
            Modified();
        }

        public void ChangeLanguageId(long languageId)
        {
            LanguageId = languageId;
            Modified();
        }

        public void ChangeIsPage(bool isPage)
        {
            IsPage = isPage;
            Modified();
        }
    }
}
