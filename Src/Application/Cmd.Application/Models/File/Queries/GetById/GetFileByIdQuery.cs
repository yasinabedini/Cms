using Cmd.Application.Common.Queries;
using Cmd.Application.Models.File.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.File.Queries.GetById
{
    public class GetFileByIdQuery : IQuery<FileViewModel>
    {
        public long Id { get; set; }
    }
}
