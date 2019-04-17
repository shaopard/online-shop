using System.Linq;

namespace onLineShop.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity GetById(object id);
        IQueryable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        void Commit();
        void Dispose();
    }
}