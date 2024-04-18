namespace Cms.Domain.Common.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(long id);
        void Add(TEntity entity);
        List<TEntity> GetList();
        void Update(TEntity entity);
        void Delete(long id);
        void Save();
        void SaveAsync();
        void Close();
        bool CheckAvailability(long id);
    }
}
