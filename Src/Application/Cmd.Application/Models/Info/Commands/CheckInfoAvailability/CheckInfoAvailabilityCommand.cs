using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Info.Commands.CheckInfoAvailability
{
    public class CheckInfoAvailabilityCommand:ICommand<bool>
    {
        public long Id { get; set; }
    }
}
