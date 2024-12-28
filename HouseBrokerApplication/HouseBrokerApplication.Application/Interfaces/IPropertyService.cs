using HouseBrokerApplication.Application.DTOs;
using HouseBrokerApplication.Domain.Entities;
using HouseBrokerApplication.Shared.Helpers;

namespace HouseBrokerApplication.Application.Interfaces;
/// <summary>
/// interface for Property service 
/// </summary>
public interface IPropertyService
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(int id);
    Task AddAsync(Property property);
    Task UpdateAsync(Property property);
    Task DeleteAsync(int id);
    Task<PaginatedList<Property>> SearchAsync(string? location, decimal? minPrice, decimal? maxPrice, string? propertyType, string? brokerContact, int pageNumber = 1, int pageSize = 10);
}
