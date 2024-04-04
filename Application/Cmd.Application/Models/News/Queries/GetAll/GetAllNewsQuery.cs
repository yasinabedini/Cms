using Cmd.Application.Common.Commands;
using Cmd.Application.Common.Queries;
using Cmd.Application.Models.News.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Queries.GetAll
{
    public class GetAllNewsQuery : PageQuery<PagedData<NewsViewModel>>
    {
        public long? TypeId { get; set; }
        public long? LanguageId { get; set; }
    }
}
