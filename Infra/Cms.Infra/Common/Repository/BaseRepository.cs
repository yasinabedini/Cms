using Cms.Domain.Common.Entities;
using Cms.Domain.Common.Repositories;
using Cms.Infra.Contexts;


namespace Cms.Infra.Common.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot
    {
        private readonly CmsDbContext _context;

        public BaseRepository(CmsDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(long id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            entity.IsDelete = true;
            Update(entity);
        }

        public TEntity GetById(long id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetList()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
