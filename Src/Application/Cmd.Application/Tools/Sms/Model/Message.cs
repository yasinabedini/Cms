using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Tools.Sms.Model
{
    public class Message
    {
        public String Text
        {
            get; set;
        }
        public String Receptor
        {
            get;
            set;
        }
        public String Sender
        {
            get; set;
        }
        public String SendDate
        {
            get; set;
        }
    }
}
