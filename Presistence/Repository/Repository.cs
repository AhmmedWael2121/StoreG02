using Microsoft.EntityFrameworkCore;
using Persistence.Data.Context;
using Store.Domain.Contracts;
using Store.Domain.Entities;
using Store.Domain.Entities.Products;
using Store.Persistence.Specifications;

namespace Store.Persistence.Repository
{
    public class Repository<TKey, TEntity>(StoreDbContext _dbContext) :
        IRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                var query = _dbContext.Products
                    .Include(b => b.Brand)
                    .Include(t => t.Type);

                return changeTracker
                    ? (await query.ToListAsync() as IEnumerable<TEntity>)!
                    : (await query.AsNoTracking().ToListAsync() as IEnumerable<TEntity>)!;
            }

            return changeTracker
                ? await _dbContext.Set<TEntity>().ToListAsync()
                : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey key)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                // Convert TKey to int for Product comparison
                var intKey = Convert.ToInt32(key);

                return await _dbContext.Products
                    .Include(b => b.Brand)
                    .Include(t => t.Type)
                    .FirstOrDefaultAsync(p => p.Id == intKey) as TEntity;
            }

            return await _dbContext.Set<TEntity>().FindAsync(key);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void UpdateAsync(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public void DeleteAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TKey, TEntity> spec, bool changeTracker = false)
        {
         return await GetSpecification(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpecification<TKey, TEntity> spec)
        {
            return await GetSpecification(spec).FirstOrDefaultAsync();

        }
        private IQueryable<TEntity> GetSpecification(ISpecification<TKey,TEntity> spec)
        {
            return SpecificationEvaluator.GetQuery(_dbContext.Set<TEntity>(), spec);
        }
    }
}