using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey>
           where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetAllQueryable(bool trackChanges = false);

        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> FindByCondition(
            Expression<Func<TEntity, bool>> expression,
            bool trackChanges = false);

        Task<TEntity?> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        // Specification support
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default);

        Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default);

        Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default);

        Task<int> CountAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default);
    }
}
