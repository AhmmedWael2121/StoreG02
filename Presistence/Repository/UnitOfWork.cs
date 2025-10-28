using Persistence.Data.Context;
using Store.Domain.Contracts;
using Store.Domain.Entities;
using System.Collections.Concurrent;
namespace Store.Persistence.Repository
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        //private Dictionary<string , object> _repositories = new Dictionary<string , object>();
        private ConcurrentDictionary<string , object> _repositories = new ConcurrentDictionary<string , object>();
        public IRepository<Tkey, TEntity> GetRepository<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>
        {
            /*Old Way*/
            //// Get entity name  
            //var type = typeof(TEntity).Name;
            //// if exist
            //if (!_repositories.ContainsKey(type))
            //    {
            //        var repo = new Repository<Tkey, TEntity>(_dbContext);
            //        _repositories.Add(type, repo);
            //    }
            //// get repo
            //return (IRepository<Tkey, TEntity>)_repositories[type];
            //return (IRepository<Tkey, TEntity>) _repositories.GetOrAdd(typeof(TEntity).Name, new Repository<Tkey,TEntity>(_dbContext));
            return (IRepository<Tkey, TEntity>)_repositories.GetOrAdd(typeof(TEntity).Name, new Repository<Tkey, TEntity>(_dbContext));
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
