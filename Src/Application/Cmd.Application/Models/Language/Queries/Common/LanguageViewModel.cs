using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Language.Queries.Common
{
    public class LanguageViewModel
    {
        public long Id { get; set; }
        public string Title { get; private set; }
        public string Name { get; private set; }
        public bool Rtl { get; private set; }
        public string Region { get; private set; }
        public bool IsEnable { get; set; }

        public LanguageViewModel(long id,string title, string name, bool rtl, string region, bool isEnable)
        {
            Id = id;
            Title = title;
            Name = name;
            Rtl = rtl;
            Region = region;
            IsEnable = isEnable;
        }
    }
}
