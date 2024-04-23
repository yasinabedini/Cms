using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Clients.AdminPanel.ViewModels
{
    public class LanguageViewModel
    {
        public long Id { get; set; }
        public string Title { get;  set; }
        public string Name { get;  set; }
        public bool Rtl { get;  set; }
        public string Region { get;  set; }
        public bool IsEnable { get; set; }

        public LanguageViewModel()
        {
            
        }
        public LanguageViewModel(string title, string name, bool rtl, string region, bool isEnable)
        {
            Title = title;
            Name = name;
            Rtl = rtl;
            Region = region;
            IsEnable = isEnable;
        }
    }
}
