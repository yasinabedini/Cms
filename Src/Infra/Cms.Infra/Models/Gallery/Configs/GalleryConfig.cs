using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Gallery.Configs
{
    public class GalleryConfig : IEntityTypeConfiguration<Domain.Models.Gallery.Entities.Gallery>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Gallery.Entities.Gallery> builder)
        {
            throw new NotImplementedException();
        }
    }
}
