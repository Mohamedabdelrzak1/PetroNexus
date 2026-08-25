using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Common;

namespace ServiceAbstraction
{
    /// <summary>
    /// Generic base contract for all application services.
    /// </summary>
    /// <typeparam name="TKey">Primary key type (usually int).</typeparam>
    /// <typeparam name="TResponseDto">The response DTO type.</typeparam>
    /// <typeparam name="TCreateDto">The create DTO type.</typeparam>
    /// <typeparam name="TUpdateDto">The update DTO type.</typeparam>
    public interface IBaseService<TKey, TResponseDto, TCreateDto, TUpdateDto>
    {
        /// <summary>Returns all records as a flat list (no pagination).</summary>
        Task<IReadOnlyList<TResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>Returns a paginated, searchable, sortable list.</summary>
        Task<PagedResult<TResponseDto>> GetAllAsync(
            string? search, int pageIndex, int pageSize,
            string? sortBy, bool sortDesc,
            CancellationToken cancellationToken = default);

        /// <summary>Returns a lightweight lookup list (id + name) for dropdowns.</summary>
        Task<IEnumerable<TResponseDto>> GetLookupAsync(CancellationToken cancellationToken = default);

        /// <summary>Returns a summary list.</summary>
        Task<IEnumerable<TResponseDto>> GetSummaryAsync(CancellationToken cancellationToken = default);

        /// <summary>Returns a single record by primary key; null if not found.</summary>
        Task<TResponseDto?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>Creates a new record and returns the created DTO.</summary>
        Task<TResponseDto> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default);

        /// <summary>Updates an existing record.</summary>
        Task UpdateAsync(TKey id, TUpdateDto dto, CancellationToken cancellationToken = default);

        /// <summary>Deletes a record by primary key.</summary>
        Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>Returns true if a record with the given key exists.</summary>
        Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
