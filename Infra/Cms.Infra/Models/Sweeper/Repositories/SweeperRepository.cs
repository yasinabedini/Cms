using Cms.Domain.Models.Sweeper.Repositories;
using Cms.Infra.Common.Repository;
using Cms.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Sweeper.Repositories
{
    public class SweeperRepository : BaseRepository<Domain.Models.Sweeper.Entities.Sweeper>, ISweeperRepository
    {
        private readonly CmsDbContext context;

        public SweeperRepository(CmsDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
