using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON.Helpers.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> ToPaged<T>(this IQueryable<T> query, int? page, int? pageSize)
        {
            if (!page.HasValue)
                page = 1;
            if (!pageSize.HasValue)
                pageSize = 20;

            if (page < 1)
                throw new InvalidDataException("The field page must be between 1 and 2147483647.");
            if (pageSize < 1)
                throw new InvalidDataException("The field pageSize must be between 1 and 2147483647.");

            return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }
    }
}
