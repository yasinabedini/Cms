using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.Contact.Queries.Common
{
    public class ContactViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }

        public ContactViewModel(string name, string email, string text)
        {
            Name = name;
            Email = email;
            Text = text;
        }
    }
}
