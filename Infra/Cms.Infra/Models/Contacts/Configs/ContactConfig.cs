using Cms.Domain.Common.ValueObjects.Conversion;
using Cms.Domain.Models.Contact.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Contacts.Configs
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(t => t.Text).HasConversion<DescriptionConversion>();
        }
    }
}
