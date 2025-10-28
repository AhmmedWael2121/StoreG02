using Store.Domain.Contracts;
using Store.Domain.Entities;
using System.Linq.Expressions;
namespace Store.Service.Specifications
{
    // Create Structure of Query of Exp
    public class BaseSpecification<TKey, TEntity>(Expression<Func<TEntity, bool>>? expression) : ISpecification<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> IncludesExp { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Filter { get; set; } = expression;
        public Expression<Func<TEntity, object>>? OrderBy { get ; set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get ; set; }
        public int Take { get; set ; }
        public int Skip { get ; set ; }
        public bool IsPagination { get; set; }
        public void Pagination(int PageNumber, int PageSize)
        {
            IsPagination = true;
            Skip = (PageNumber - 1) * PageSize;
            Take = PageSize;
        }
    }
}
