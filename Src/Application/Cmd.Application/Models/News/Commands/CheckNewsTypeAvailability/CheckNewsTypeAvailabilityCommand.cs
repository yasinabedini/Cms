using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.CheckNewsTypeAvailability
{
    public class CheckNewsTypeAvailabilityCommand:ICommand<bool>
    {
        public long Id { get; set; }
    }
}
