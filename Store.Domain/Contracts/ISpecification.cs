using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface ISpecification<Tkey,TEntity> where TEntity : BaseEntity<Tkey>
    {
        // list of include expressions  
        List<Expression<Func<TEntity,object>>> IncludesExp { get; set; }  
        Expression<Func<TEntity, bool>> Filter { get; set; }  
        Expression<Func<TEntity, object>> OrderBy { get; set; }  
        Expression<Func<TEntity, object>> OrderByDesc { get; set; }
        int Take { get; set; }
        int Skip { get; set; }
        bool IsPagination { get; set; }
    }
}
