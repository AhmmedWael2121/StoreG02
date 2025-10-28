using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface IRepository<TKey,TEntity> where TEntity : BaseEntity<TKey>
    {
        // tracker is a boolean parameter to indicate whether to use change tracking or not
        public Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false); 
        public Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TKey,TEntity> spec,bool changeTracker = false); 
        public Task<TEntity?> GetAsync(TKey key);
        public Task<TEntity?> GetAsync(ISpecification<TKey,TEntity> spec);
        public Task AddAsync (TEntity entity);
        public void UpdateAsync (TEntity entity);
        public void DeleteAsync (TEntity entity);

        

    }
}
