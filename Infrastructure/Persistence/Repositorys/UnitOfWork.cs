using Domain.Common;
using Domain.Contracts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PetroNexusDbContext _context;
        private readonly Hashtable _repositories = new();
        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(PetroNexusDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
        {
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repositoryInstance = new GenericRepository<TEntity, TKey>(_context);
                _repositories.Add(key, repositoryInstance);
            }
            return (IGenericRepository<TEntity, TKey>)_repositories[key]!;
        }

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
        {
            return GetRepository<TEntity, TKey>();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                return _currentTransaction;
            }
            _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return _currentTransaction;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync(cancellationToken);
                }
            }
            catch
            {
                await RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.RollbackAsync(cancellationToken);
                }
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void ClearTracking()
        {
            _context.ChangeTracker.Clear();
        }

        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
