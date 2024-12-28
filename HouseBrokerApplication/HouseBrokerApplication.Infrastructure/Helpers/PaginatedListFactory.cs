using HouseBrokerApplication.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HouseBrokerApplication.Infrastructure.Helpers;

public static class PaginatedListFactory
{
    public static async Task<PaginatedList<T>> CreateAsync<T>(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
