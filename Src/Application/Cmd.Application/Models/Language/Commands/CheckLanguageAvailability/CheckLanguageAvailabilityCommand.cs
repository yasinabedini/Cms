using Cmd.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Language.Commands.CheckLanguageAvailability
{
    public class CheckLanguageAvailabilityCommand:ICommand<bool>
    {
        public long Id { get; set; }
    }
}
