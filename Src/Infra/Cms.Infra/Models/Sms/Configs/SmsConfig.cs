using Cms.Domain.Common.ValueObjects.Conversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Sms.Configs
{
    public class SmsConfig : IEntityTypeConfiguration<Domain.Models.Sms.Entities.Sms>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Sms.Entities.Sms> builder)
        {
            builder.Property(t => t.PhoneNumber).HasConversion<PhoneNumberConversion>();
        }
    }
}
