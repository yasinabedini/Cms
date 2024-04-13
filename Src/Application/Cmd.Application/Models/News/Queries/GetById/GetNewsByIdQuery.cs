using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetById
{
    public class GetNewsByIdQuery:IQuery<NewsViewModel>
    {
        public long Id { get; set; }        
    }
}
