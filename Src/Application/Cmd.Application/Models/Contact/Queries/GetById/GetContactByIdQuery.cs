using Cmd.Application.Common.Queries;
using Cmd.Application.Models.Contact.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Contact.Queries.GetById
{
    public class GetContactByIdQuery:IQuery<ContactViewModel>
    {
        public long Id { get; set; }
    }
}
