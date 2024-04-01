using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Sweeper.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Queries.GetAll
{
    public class GetAllSweeperQuery:PageQuery<PagedData<SweeperViewModel>>
    {
        public int LanguageId { get; set; }
    }
}
