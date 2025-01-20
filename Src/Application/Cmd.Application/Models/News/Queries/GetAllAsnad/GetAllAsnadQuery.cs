using Cmd.Application.Common.Queries;
using Cms.Domain.Models.News.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetAllAsnad
{
    public class GetAllAsnadQuery : PageQuery<PagedData<Asnad>>
    {
        public string Alephbatic { get; set; }
    }
}
