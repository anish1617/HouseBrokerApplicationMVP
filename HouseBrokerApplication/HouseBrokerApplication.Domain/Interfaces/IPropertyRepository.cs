
using HouseBrokerApplication.Domain.Entities;
using HouseBrokerApplication.Shared.Helpers;

namespace HouseBrokerApplication.Domain.Interfaces;
/// <summary>
/// Defines the contract for property repository operations 
/// </summary>
public interface IPropertyRepository
{
    /// <summary>
    /// Fetch all the properties 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Property>> GetAllAsync();
    /// <summary>
    /// Get specific property details by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Property?> GetByIdAsync(int id);
    /// <summary>
    /// create a new property 
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    Task AddAsync(Property property);
    /// <summary>
    /// updates the property details
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    Task UpdateAsync(Property property);
    /// <summary>
    /// deletes property from the list
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(int id);

    /// <summary>
    /// retrieves a paginated list of properties based on
    /// search by location, price range, propertytype and brokerContact and return paginatedList with additional information
    /// </summary>
    /// <param name="location"></param>
    /// <param name="minPrice"></param>
    /// <param name="maxPrice"></param>
    /// <param name="propertyType"></param>
    /// <returns></returns>
    Task<PaginatedList<Property>> SearchAsync(string? location, decimal? minPrice, decimal? maxPrice, string? propertyType, string? brokerContact, int pageNumber = 1, int pageSize = 10);

}
