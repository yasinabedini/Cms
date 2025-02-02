﻿using Cms.Domain.Common.Entities;
using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.Language.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Sweeper.Entities
{
    public class Sweeper : AggregateRoot
    {
        public Title Title { get;private set; }
        public Description Text { get; private set; }
        public string Link { get; private set; }
        public string ImageName { get; private set; }        
        public long LanguageId { get; private set; }

        public Language.Entities.Language Language { get;private set; }

        protected Sweeper() { }
        private Sweeper(Title title, Description text, string link, string imageName, long languageId)
        {
            Title = title;
            Text = text;
            Link = link;
            ImageName = imageName;            
            LanguageId = languageId;            
        }

        public static Sweeper Create(Title title, Description text, string link, string imageName, long languageId)
        {
            return new Sweeper(title,text, link, imageName, languageId);
        }

        public void ChangeTitle(string title)
        {
            Title = title;
            Modified();
        }

        public void ChangeText(string text)
        {
            Text = text;
            Modified();
        }

        public void ChangeLink(string link)
        {
            Link = link;
            Modified();
        }

        public void ChangeImageName(string imageName)
        {
            ImageName = imageName;
            Modified();
        }

        public void ChangeLanguageId(long languageId)
        {
            LanguageId = languageId;
            Modified();
        }
    }
}
