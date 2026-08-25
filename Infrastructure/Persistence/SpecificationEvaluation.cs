using Domain.Common;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationEvaluation
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TKey>(
            IQueryable<TEntity> inputQuery,
            ISpecifications<TEntity, TKey> spec)
            where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.IncludeExpressions
                      .Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }

        public static IQueryable<TEntity> GetQuery<TEntity, TKey>(
            IQueryable<TEntity> inputQuery,
            ISpecification<TEntity, TKey> spec)
            where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;

            if (spec.AsNoTracking)
            {
                query = query.AsNoTracking();
            }
            else
            {
                query = query.AsTracking();
            }

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.IncludeExpressions
                      .Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            query = spec.IncludeStrings
                      .Aggregate(query, (currentQuery, includeString) => currentQuery.Include(includeString));

            return query;
        }
    }
}

