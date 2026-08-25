using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistence;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
            return await query.ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetAllQueryable(bool trackChanges = false)
        {
            return trackChanges ? _dbSet : _dbSet.AsNoTracking();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            if (id is null) return null;
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities is null) throw new ArgumentNullException(nameof(entities));
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities is null) throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities is null) throw new ArgumentNullException(nameof(entities));
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<TEntity> FindByCondition(
            Expression<Func<TEntity, bool>> expression,
            bool trackChanges = false)
        {
            IQueryable<TEntity> query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
            return query.Where(expression);
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
            if (include is not null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        // Specification implementation
        public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec).ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            return await query.AnyAsync(cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            return await query.CountAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default)
        {
            return await AnyAsync(spec, cancellationToken);
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TKey> spec)
        {
            return SpecificationEvaluation.GetQuery(_dbSet.AsQueryable(), spec);
        }
    }
}