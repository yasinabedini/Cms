using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cmd.Application.Security
{
    public static class InputSanitizer
    {
        public static string SanitizeInput(string input)
        {
            string sanitizedInput = Regex.Replace(input, @"[^\w\.@-]", "");
            return sanitizedInput;
        }
    }
}
