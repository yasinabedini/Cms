using Cms.Domain.Common.ValueObjects.Conversion;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Page.Configs
{
    public class PageConfiguration : IEntityTypeConfiguration<Domain.Models.Page.Entities.Page>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Page.Entities.Page> builder)
        {
            builder.Property(t => t.Title).HasConversion<TitleConversion>();
            builder.Property(t => t.Text).HasConversion<DescriptionConversion>();
        }
    }
}
