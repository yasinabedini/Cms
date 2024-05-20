using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetAsnad
{
    public class GetAsnadQuery:IQuery<List<AsnadViewModel>>
    {
        public long LanguageId { get; set; }
    }
}
