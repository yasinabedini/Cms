using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Sweeper.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Sweeper.Queries.GetById
{
    public class GetSweeperByIdQuery:IQuery<SweeperViewModel>
    {
        public int Id { get; set; }
    }
}
