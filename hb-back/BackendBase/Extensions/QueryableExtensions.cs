using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Extensions;

public static class QueryableExtensions
{
    public static async Task<Pagination<T>> Search<T>(this IQueryable<T> dbset, SearchDto searchDto)
    {
        var count = dbset.Count();
        var itemsQuery = dbset.Skip((searchDto.PageNumber - 1) * searchDto.PageSize).Take(searchDto.PageSize)
            .AsQueryable();

        return new Pagination<T>
        {
            PageNumber = searchDto.PageNumber,
            PageSize = searchDto.PageSize,
            TotalPages = count / searchDto.PageSize + (count % searchDto.PageSize != 0 ? 1 : 0),
            Entities = await itemsQuery.ToListAsync()
        };
    }
}