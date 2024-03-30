using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Language.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Language.Queries.GetById
{
    public class GetLanguageByIdQuery:IQuery<LanguageViewModel>
    {
        public int Id { get; set; }
    }
}
