using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Extensions;

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
            PageNumber: dto.PageNumber,
            PageSize: dto.PageSize,
            Entities: await collectionSlice.ToListAsync(),
            CollectionLength: collectionLength
        );
    }
}
