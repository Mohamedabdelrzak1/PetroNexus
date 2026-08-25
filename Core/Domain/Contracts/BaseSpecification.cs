using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Common;

namespace Domain.Contracts
{
    public abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();
        public List<string> IncludeStrings { get; } = new();
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagination { get; private set; }
        public bool AsNoTracking { get; private set; } = true;

        protected BaseSpecification()
        {
        }

        protected BaseSpecification(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
            OrderByDescending = null;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
            OrderBy = null;
        }

        protected void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagination = true;
        }

        protected void SetTracking(bool track)
        {
            AsNoTracking = !track;
        }
    }
}
