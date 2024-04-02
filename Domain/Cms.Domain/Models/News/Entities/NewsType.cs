using Cms.Domain.Common.Entities;
using Cms.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
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
        #endregion

        #region Constructors and Factories
        protected NewsType() { }
        private NewsType(Title title, Title name)
        {
            Title = title;
            Name = name;
        }

        public static NewsType Create(Title title, Title name)
        {
            return new NewsType(title, name);
        } 
        #endregion
    }
}
