using Cms.Domain.Models.Contact.Entities;
using Cms.Domain.Models.Contact.Repositories;
using Cms.Infra.Common.Repository;
using Cms.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Contacts.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(CmsDbContext context) : base(context)
        {
        }
    }
}
