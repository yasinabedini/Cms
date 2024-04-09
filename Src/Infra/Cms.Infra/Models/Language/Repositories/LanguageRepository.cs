using Cms.Domain.Models.Language.Repositories;
using Cms.Infra.Common.Repository;
using Cms.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.Language.Repositories
{
    public class LanguageRepository : BaseRepository<Domain.Models.Language.Entities.Language>, ILanguageRepository
    {
        public LanguageRepository(CmsDbContext context) : base(context)
        {
        }
    }
}
