using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Info.Configs
{
    public class InfoConfig : IEntityTypeConfiguration<Domain.Models.Info.Entities.Info>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Info.Entities.Info> builder)
        {
   
        }
    }
}
