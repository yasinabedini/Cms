using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.File.Configs
{
    public class FileConfig : IEntityTypeConfiguration<Domain.Models.File.Entities.File>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.File.Entities.File> builder)
        {
            throw new NotImplementedException();
        }
    }
}
