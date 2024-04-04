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

        #endregion

        #region Constructors and Factories
        protected NewsType() { }
        private NewsType(Title title, Title name, long languageId)
        {
            Title = title;
            Name = name;
            LanguageId = languageId;
        }

        public static NewsType Create(Title title, Title name,long languageId)
        {
            return new NewsType(title, name, languageId);
        } 
        #endregion
    }
}
