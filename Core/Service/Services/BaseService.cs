using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Common;
using Domain.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ServiceAbstraction;
using Shared.Common;

namespace Service.Services
{
    public abstract class BaseService<TEntity, TKey, TResponseDto, TCreateDto, TUpdateDto>
        : IBaseService<TKey, TResponseDto, TCreateDto, TUpdateDto>
        where TEntity : BaseEntity<TKey>
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IValidator<TCreateDto>? CreateValidator;
        protected readonly IValidator<TUpdateDto>? UpdateValidator;
        protected readonly IGenericRepository<TEntity, TKey> Repository;

        protected BaseService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<TCreateDto>? createValidator = null,
            IValidator<TUpdateDto>? updateValidator = null)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            CreateValidator = createValidator;
            UpdateValidator = updateValidator;
            Repository = UnitOfWork.Repository<TEntity, TKey>();
        }

        #region Hooks
        protected virtual Task BeforeCreateAsync(TCreateDto dto, TEntity entity, CancellationToken cancellationToken)
            => Task.CompletedTask;

        protected virtual Task AfterCreateAsync(TEntity entity, TResponseDto responseDto, CancellationToken cancellationToken)
            => Task.CompletedTask;

        protected virtual Task BeforeUpdateAsync(TUpdateDto dto, TEntity entity, CancellationToken cancellationToken)
            => Task.CompletedTask;

        protected virtual Task AfterUpdateAsync(TEntity entity, CancellationToken cancellationToken)
            => Task.CompletedTask;

        protected virtual Task BeforeDeleteAsync(TEntity entity, CancellationToken cancellationToken)
            => Task.CompletedTask;

        protected virtual Task AfterDeleteAsync(TEntity entity, CancellationToken cancellationToken)
            => Task.CompletedTask;
        #endregion

        /// <inheritdoc/>
        public virtual async Task<IReadOnlyList<TResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await Repository.GetAllAsync(trackChanges: false, cancellationToken: cancellationToken);
            return Mapper.Map<IReadOnlyList<TResponseDto>>(entities);
        }

        /// <inheritdoc/>
        public virtual async Task<PagedResult<TResponseDto>> GetAllAsync(
            string? search,
            int pageIndex,
            int pageSize,
            string? sortBy,
            bool sortDesc,
            CancellationToken cancellationToken = default)
        {
            // ✅ Build IQueryable first — filtering, sorting, and pagination
            // all happen in SQL, NOT in memory.
            IQueryable<TEntity> query = Repository.GetAllQueryable(trackChanges: false);

            // Generic sort by property name using reflection (translated to SQL by EF Core)
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var prop = typeof(TEntity).GetProperty(
                    sortBy,
                    System.Reflection.BindingFlags.IgnoreCase |
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Instance);

                if (prop != null)
                {
                    var parameter = System.Linq.Expressions.Expression.Parameter(typeof(TEntity), "x");
                    var propertyAccess = System.Linq.Expressions.Expression.Property(parameter, prop);
                    var lambda = System.Linq.Expressions.Expression.Lambda(propertyAccess, parameter);

                    var methodName = sortDesc ? "OrderByDescending" : "OrderBy";
                    var resultType = typeof(IOrderedQueryable<TEntity>);

                    var method = typeof(Queryable).GetMethods()
                        .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
                        .Single()
                        .MakeGenericMethod(typeof(TEntity), prop.PropertyType);

                    query = (IQueryable<TEntity>)method.Invoke(null, new object[] { query, lambda })!;
                }
            }

            // ✅ Count total BEFORE pagination (in SQL, not memory)
            var totalCount = await query.CountAsync(cancellationToken);

            // ✅ Apply pagination in SQL (Skip/Take on IQueryable before ToListAsync)
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<TResponseDto>
            {
                Items = Mapper.Map<IReadOnlyList<TResponseDto>>(items),
                TotalCount = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TResponseDto>> GetLookupAsync(CancellationToken cancellationToken = default)
        {
            var entities = await Repository.GetAllAsync(trackChanges: false, cancellationToken: cancellationToken);
            return Mapper.Map<IEnumerable<TResponseDto>>(entities);
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TResponseDto>> GetSummaryAsync(CancellationToken cancellationToken = default)
        {
            var entities = await Repository.GetAllAsync(trackChanges: false, cancellationToken: cancellationToken);
            return Mapper.Map<IEnumerable<TResponseDto>>(entities);
        }

        /// <inheritdoc/>
        public virtual async Task<TResponseDto?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            if (entity is null) return default;
            return Mapper.Map<TResponseDto>(entity);
        }

        /// <inheritdoc/>
        public virtual async Task<TResponseDto> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            if (CreateValidator is not null)
            {
                var result = await CreateValidator.ValidateAsync(dto, cancellationToken);
                if (!result.IsValid)
                    throw new ValidationException(result.Errors);
            }

            var entity = Mapper.Map<TEntity>(dto);
            await BeforeCreateAsync(dto, entity, cancellationToken);

            await Repository.AddAsync(entity, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            var responseDto = Mapper.Map<TResponseDto>(entity);
            await AfterCreateAsync(entity, responseDto, cancellationToken);

            return responseDto;
        }

        /// <inheritdoc/>
        public virtual async Task UpdateAsync(TKey id, TUpdateDto dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            if (UpdateValidator is not null)
            {
                var result = await UpdateValidator.ValidateAsync(dto, cancellationToken);
                if (!result.IsValid)
                    throw new ValidationException(result.Errors);
            }

            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} was not found.");

            Mapper.Map(dto, entity);
            await BeforeUpdateAsync(dto, entity, cancellationToken);

            Repository.Update(entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            await AfterUpdateAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} was not found.");

            await BeforeDeleteAsync(entity, cancellationToken);

            Repository.Delete(entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            await AfterDeleteAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            return entity is not null;
        }

        // ─── Specification-based helpers (not part of IBaseService) ─────────

        public virtual async Task<IReadOnlyList<TResponseDto>> GetAsync(
            ISpecification<TEntity, TKey> spec,
            CancellationToken cancellationToken = default)
        {
            var entities = await Repository.ListAsync(spec, cancellationToken);
            return Mapper.Map<IReadOnlyList<TResponseDto>>(entities);
        }

        public virtual async Task<TResponseDto?> GetFirstOrDefaultAsync(
            ISpecification<TEntity, TKey> spec,
            CancellationToken cancellationToken = default)
        {
            var entity = await Repository.FirstOrDefaultAsync(spec, cancellationToken);
            return entity is null ? default : Mapper.Map<TResponseDto>(entity);
        }

        public virtual async Task<int> CountAsync(
            ISpecification<TEntity, TKey> spec,
            CancellationToken cancellationToken = default)
        {
            return await Repository.CountAsync(spec, cancellationToken);
        }
    }
}
