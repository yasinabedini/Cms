using Cms.Domain.Common.ValueObjects.Conversion;
using Cms.Domain.Models.News.Entities;
using Cms.Domain.Models.News.ValueObjects.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.News.Configs
{
    public class NewsConfig : IEntityTypeConfiguration<Domain.Models.News.Entities.News>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.News.Entities.News> builder)
        {
            builder.Property(t => t.Title).HasConversion<TitleConversion>().IsRequired();                       
            builder.Property(t => t.MainImageName).HasConversion<ImageConversion>().IsRequired();
            builder.Property(t => t.SecondImage).HasConversion<ImageConversion>();
            builder.Property(t => t.ThirdImage).HasConversion<ImageConversion>();
            builder.Property(t => t.ThumbNailImage).HasConversion<ImageConversion>();
            builder.Property(t => t.Introduction).HasConversion<DescriptionConversion>();            
        }
    }
}
