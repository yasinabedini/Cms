using Cms.Domain.Models.Info.Repositories;
using Cms.Infra.Common.Repository;
using Cms.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Info.Reositories
{
    public class InfoRepository : BaseRepository<Domain.Models.Info.Entities.Info>, IInfoRepository
    {
        public InfoRepository(CmsDbContext context) : base(context)
        {
        }
    }
}
