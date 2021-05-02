using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.SharedKernal
{
    public class QueryResult<T> where T : class
    {
        public IEnumerable<T> Items { get; private set; }

        /// <summary>
        /// Gets total number of items (useful when paging is used, otherwise 0)
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Gets current page nubmer used to get items (useful when paging is used)
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// Get page size used to get items (useful when paging is used)
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResult{T}" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="totalCount">The total count (if paging is used, otherwise <c>0</c>).</param>
        /// <param name="pageNumber">The page number (if paging is used, otherwise <c>0</c>).</param>
        /// <param name="pageSize">The page size (if paging is used, otherwise <c>0</c>).</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public QueryResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            if (totalCount < 0)
                throw new ArgumentOutOfRangeException("totalCount", totalCount, "Incorrect value.");

            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
