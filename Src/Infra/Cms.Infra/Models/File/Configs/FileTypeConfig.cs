using Cms.Domain.Models.File.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.File.Configs
{
    public class FileTypeConfig : IEntityTypeConfiguration<Domain.Models.File.Entities.FileType>
    {
        public void Configure(EntityTypeBuilder<FileType> builder)
        {
            builder.HasOne(t => t.Parent).WithMany().HasForeignKey(t=>t.ParentId);
        }
    }
}
