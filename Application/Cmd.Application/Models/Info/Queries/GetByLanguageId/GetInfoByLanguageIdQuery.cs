using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Info.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Queries.GetByLanguageId
{
    public class GetInfoByLanguageIdQuery:IQuery<InfoViewModel>
    {
        public long LanguageId { get; set; }
    }
}
