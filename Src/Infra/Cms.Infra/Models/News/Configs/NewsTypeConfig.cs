using Cms.Domain.Common.ValueObjects.Conversion;
using Cms.Domain.Models.News.ValueObjects.Conversions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.News.Configs
{
    public class NewsTypeConfig : IEntityTypeConfiguration<Domain.Models.News.Entities.NewsType>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.News.Entities.NewsType> builder)
        {
            builder.Property(t => t.Title).HasConversion<TitleConversion>().IsRequired();
            builder.Property(t => t.Name).HasConversion<TitleConversion>().IsRequired();

        }
    }
}
