using HouseBrokerApplication.Domain.Entities;
using HouseBrokerApplication.Domain.Interfaces;
using HouseBrokerApplication.Infrastructure.Helpers;
using HouseBrokerApplication.Infrastructure.Persistence;
using HouseBrokerApplication.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HouseBrokerApplication.Infrastructure.Repositories;

/// <summary>
/// Implements database operations for Property.
/// </summary>
public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _context;

    public PropertyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Property>> GetAllAsync() => await _context.Properties.ToListAsync();
        

    public async Task<Property?> GetByIdAsync(int id) => await _context.Properties.FindAsync(id);
    
    public async Task AddAsync(Property property)
    {
        await _context.Properties.AddAsync(property);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Property property) 
    {
        _context.Properties.Update(property);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property is null) return;

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
    }

    public async Task<PaginatedList<Property>> SearchAsync(string? location, decimal? minPrice, decimal? maxPrice, string? propertyType, string? brokerContact, int pageNumber = 1, int pageSize = 10)
    {
        IQueryable<Property> query = _context.Properties;

        if (!string.IsNullOrEmpty(location))
            query = query.Where(p => p.Location.Contains(location));

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice);

        if (!string.IsNullOrEmpty(propertyType))
            query = query.Where(p => p.PropertyType.Contains(propertyType));

        if (!string.IsNullOrEmpty(brokerContact))
            query = query.Where(p => p.BrokerContact.Equals(brokerContact));

        var dtoQuery = query.Select(p => new Property
        {
            PropertyType = p.PropertyType,
            Location = p.Location,
            Price = p.Price,
            Features = p.Features,
            Description = p.Description,
            BrokerContact = p.BrokerContact,
            ImageUrl = p.ImageUrl
        });
        var properties = await PaginatedListFactory.CreateAsync(dtoQuery, pageNumber, pageSize);
        return properties;
    }


}
