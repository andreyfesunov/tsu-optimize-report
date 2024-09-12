using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Data.Extensions;

public static class QueryableExtensions
{
    public static async Task<Pagination<T>> Search<T>(this IQueryable<T> dbset, Search search)
    {
        var collectionLength = dbset.Count();
        var collectionSlice = dbset
            .Skip((search.PageNumber - 1) * search.PageSize)
            .Take(search.PageSize)
            .AsQueryable();

        return new Pagination<T>(
            search.PageNumber,
            search.PageSize,
            await collectionSlice.ToListAsync(),
            collectionLength
        );
    }
}