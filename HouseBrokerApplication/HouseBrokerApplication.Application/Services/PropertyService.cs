using HouseBrokerApplication.Application.Interfaces;
using HouseBrokerApplication.Domain.Entities;
using HouseBrokerApplication.Domain.Interfaces;
using HouseBrokerApplication.Shared.Helpers;

namespace HouseBrokerApplication.Application.Services;

/// <summary>
/// Represents service layer for managing property listings.
/// Handles business logic related to properties
/// </summary>
public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }
    /// <summary>
    /// retrieves all the properties
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _propertyRepository.GetAllAsync();
    }
    /// <summary>
    /// retrieves properties by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Property?> GetByIdAsync(int id)
    {
        return await _propertyRepository.GetByIdAsync(id);
    }
    /// <summary>
    /// creates a new property
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    public async Task AddAsync(Property property)
    {
         await _propertyRepository.AddAsync(property);
    }
    /// <summary>
    /// deletes an existing property
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        await _propertyRepository.DeleteAsync(id); 
    }

    /// <summary>
    /// Updates an existing property
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    public async Task UpdateAsync(Property property)
    {
        await _propertyRepository.UpdateAsync(property);
    }

    /// <summary>
    /// Retrieves a paginated list of properties based on 
    /// Search results provided inputs, like location, price range, property type, and broker contact
    /// </summary>
    /// <param name="location"></param>
    /// <param name="minPrice"></param>
    /// <param name="maxPrice"></param>
    /// <param name="propertyType"></param>
    /// <param name="brokerContact"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns>A paginated list of properties</returns>
    public async Task<PaginatedList<Property>> SearchAsync(string? location, decimal? minPrice, decimal? maxPrice, string? propertyType, string? brokerContact, int pageNumber = 1, int pageSize = 10)
    {
        return await _propertyRepository.SearchAsync(location, minPrice, maxPrice, propertyType, brokerContact, pageNumber, pageSize);
    }
}
