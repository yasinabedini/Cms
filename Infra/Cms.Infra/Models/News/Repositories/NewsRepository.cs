using Cms.Domain.Common.Repositories;
using Cms.Domain.Models.News.Entities;
using Cms.Domain.Models.News.Repository;
using Cms.Infra.Common.Repository;
using Cms.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Models.News.Repositories
{
    public class NewsRepository : BaseRepository<Domain.Models.News.Entities.News>, INewsRepository, INewsTypeRepository
    {
        private readonly CmsDbContext _context;
        public NewsRepository(CmsDbContext context) : base(context)
        {
            _context = context;
        }

        public void Add(NewsType entity)
        {
            _context.Add(entity);
        }

        public void Update(NewsType entity)
        {
            _context.Update(entity);
        }

        NewsType IRepository<NewsType>.GetById(long id)
        {
            return _context.NewsTypes.Find(id);
        }

        List<NewsType> IRepository<NewsType>.GetList()
        {
            return _context.NewsTypes.ToList();
        }
    }
}
