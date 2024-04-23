using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Clients.AdminPanel.ViewModels
{
    public class ContactViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }

        public ContactViewModel()
        {
            
        }
        public ContactViewModel(long id, string name, string email, string text)
        {
            Name = name;
            Email = email;
            Text = text;
            Id = id;
        }
    }
}
