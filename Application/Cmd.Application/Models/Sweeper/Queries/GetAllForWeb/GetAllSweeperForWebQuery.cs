using Cmd.Application.Common.Commands;
using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Sweeper.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Queries.GetAllForWeb
{
    public class GetAllSweeperForWebQuery:PageQuery<PagedData<SweeperViewModel>>
    {
        public int LanguageId { get; set; }
    }
}
