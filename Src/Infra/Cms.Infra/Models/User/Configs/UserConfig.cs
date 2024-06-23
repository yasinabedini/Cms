using Cms.Domain.Common.ValueObjects.Conversion;
using Cms.Domain.Models.User.ValueObjects.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.User.Configs
{
    public class UserConfig : IEntityTypeConfiguration<Domain.Models.User.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.User.Entities.User> builder)
        {
            builder.Property(t => t.PhoneNumber).HasConversion<PhoneNumberConversion>().IsRequired();
            builder.Property(t => t.FirstName).HasConversion<NameConversion>();
            builder.Property(t => t.LastName).HasConversion<NameConversion>();
        }
    }
}
