using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Common;

namespace Domain.Contracts
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagination { get; }
        bool AsNoTracking { get; }
    }
}
