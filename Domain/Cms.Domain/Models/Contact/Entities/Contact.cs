using Cms.Domain.Common.Entities;
using Cms.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Contact.Entities
{
    public class Contact : AggregateRoot
    {
        #region Properties
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Description Text { get; private set; }
        #endregion

        #region Constructors And Factories
        protected Contact() { }
        private Contact(string name, string email, Description text)
        {
            Name = name;
            Email = email;
            Text = text;
        }
        public static Contact Create(string name, string email, Description text)
        {
            return new Contact(name, email, text);
        }
        #endregion
    }
}
