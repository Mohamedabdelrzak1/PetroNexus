using System.Collections.Generic;

namespace Shared.Common
{
    /// <summary>
    /// Represents a paginated, searchable response.
    /// </summary>
    /// <typeparam name="T">The DTO type for each item in the page.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>Items in the current page.</summary>
        public IReadOnlyList<T> Items { get; set; } = new List<T>();

        /// <summary>Total number of records across all pages.</summary>
        public int TotalCount { get; set; }

        /// <summary>Current page index (1-based).</summary>
        public int PageIndex { get; set; }

        /// <summary>Number of items per page.</summary>
        public int PageSize { get; set; }

        /// <summary>Total pages available.</summary>
        public int TotalPages => PageSize > 0 ? (TotalCount + PageSize - 1) / PageSize : 0;

        /// <summary>Whether a previous page exists.</summary>
        public bool HasPreviousPage => PageIndex > 1;

        /// <summary>Whether a next page exists.</summary>
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
