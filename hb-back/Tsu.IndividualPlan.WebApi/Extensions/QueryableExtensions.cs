using Microsoft.EntityFrameworkCore;
using Tsu.IndividualPlan.WebApi.Dto;

namespace Tsu.IndividualPlan.WebApi.Extensions;

public static class QueryableExtensions
{
    public static async Task<Pagination<T>> Search<T>(this IQueryable<T> dbset, SearchDto dto)
    {
        var collectionLength = dbset.Count();
        var collectionSlice = dbset
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .AsQueryable();

        return new Pagination<T>(
            dto.PageNumber,
            dto.PageSize,
            await collectionSlice.ToListAsync(),
            collectionLength
        );
    }
}