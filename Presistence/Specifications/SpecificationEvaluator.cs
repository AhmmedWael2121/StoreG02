using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.Domain.Contracts;
using Store.Domain.Entities;
namespace Store.Persistence.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<Tkey, TEntity>(
            IQueryable<TEntity> inputQuery, ISpecification<Tkey, TEntity> spec
            ) where TEntity : BaseEntity<Tkey>
        {
            //check Criteria fiter 
            var query = inputQuery;
            if(spec.Filter is not null)
            {
                query = query.Where(spec.Filter);
            }
            //check Criteria order 
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDesc is not null) 
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }
            //check about list of expression and concat them 

            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

                query = spec.IncludesExp.Aggregate(query, (query, IncludeExpression) => query.Include(IncludeExpression));
            return query;
        }
    }
}
