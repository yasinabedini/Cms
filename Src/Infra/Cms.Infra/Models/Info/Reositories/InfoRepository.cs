using Cms.Domain.Common.Repositories;
using Cms.Domain.Models.Info.Entities;
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
    public class InfoRepository : BaseRepository<Domain.Models.Info.Entities.Info>, IInfoRepository,IInfoLinkRepository
    {
        private readonly CmsDbContext _context;
        public InfoRepository(CmsDbContext context) : base(context)
        {
            _context = context;
        }

        public void Add(InfoLink entity)
        {
            _context.Add(entity);
        }

        public void Update(InfoLink entity)
        {
            _context.Update(entity);
        }

        InfoLink IRepository<InfoLink>.GetById(long id)
        {
            return _context.InfoLink.FirstOrDefault(t => t.Id == id);
        }

        List<InfoLink> IRepository<InfoLink>.GetList()
        {
            var result = _context.InfoLink.ToList();
            return result;
        }
    }
}
