using Cms.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Language.Entities
{
    public class Language : AggregateRoot
    {
        #region Properties
        public string Title { get; private set; }
        public string Name { get; private set; }
        public bool Rtl { get; private set; }
        public string Region { get; private set; }
        #endregion

        #region Constructors and Factories
        protected Language() { }
        private Language(string title, string name, bool rtl, string region)
        {
            Title = title;
            Name = name;
            Rtl = rtl;
            Region = region;
        }

        public static Language Create(string title, string name, bool rtl, string region)
        {
            return new Language(title, name, rtl, region);
        }
        #endregion

        #region Methods
        public void SetId(long id)
        {
            Id = id;            
        }

        public void ChangeName(string name)
        {
            Name = name;
            Modified();
        }

        public void ChangeTitle(string title)
        {
            Title = title;
            Modified();
        }

        public void ChnageRegion(string region)
        {
            Region = region;
            Modified();
        }

        public void ChangeToLtr()
        {
            if (Rtl == true)
            {
                Rtl = false;
                Modified();
            }
        }

        public void ChangeToRtl()
        {
            if (Rtl == false)
            {
                Rtl = true;
                Modified();
            }
        }
        #endregion
    }
}
