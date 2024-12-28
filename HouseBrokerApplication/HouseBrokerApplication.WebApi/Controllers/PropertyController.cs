using HouseBrokerApplication.Application.DTOs;
using HouseBrokerApplication.Application.Interfaces;
using HouseBrokerApplication.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseBrokerApplication.WebApi.Controllers;

/// <summary>
/// handles https request for Property API
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IWebHostEnvironment _environment;

    public PropertyController(IPropertyService repository, IWebHostEnvironment environment)
    {
        _propertyService = repository;
        _environment = environment;
    }

    /// <summary>
    /// Retrieves all properties
    /// </summary>
    /// <returns>List of properties</returns>
    [HttpGet]
    [Authorize(Roles = "Seeker,Broker")]
    public async Task<IActionResult> GetAll() 
    {
        var properties = await _propertyService.GetAllAsync();
        if (properties.Any())
        {
            foreach (var property in properties)
            {
                if (!string.IsNullOrEmpty(property.ImageUrl))
                {
                    var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{Path.GetFileName(property.ImageUrl)}";

                    // Set the full URL for ImageUrl before returning
                    property.ImageUrl = imageUrl;
                }
            }
        }
        return Ok(properties);
    }

    /// <summary>
    /// Retrieves a property detail by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Seeker,Broker")]
    public async Task<IActionResult> GetById(int id)
    {
        var property = await _propertyService.GetByIdAsync(id);
        if (property is null) return NotFound();

        if (!string.IsNullOrEmpty(property.ImageUrl))
        {
            var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{Path.GetFileName(property.ImageUrl)}";

            // Set the full URL for ImageUrl before returning
            property.ImageUrl = imageUrl;
        }
        return Ok(property);
    }

    /// <summary>
    /// Creates a new property: Only Broker can create
    /// </summary>
    /// <param name="propertyDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Broker")]
    public async Task<IActionResult> Create([FromForm] PropertyCreateDto propertyDto)
    {
        // handle image uploads 
        string imagePath = await SaveImage(propertyDto.Image);
        var property = new Property
        {
            PropertyType = propertyDto.PropertyType,
            Location = propertyDto.Location,
            Price = propertyDto.Price,
            Features = propertyDto.Features,
            Description = propertyDto.Description,
            BrokerContact = propertyDto.BrokerContact,
            ImageUrl = imagePath
        };

        await _propertyService.AddAsync(property);
        return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
    }

    /// <summary>
    /// updates the property if exists based on updated data by its id
    /// Only Broker can Update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="propertyDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Broker")]
    public async Task<IActionResult> Update(int id, [FromBody] PropertyUpdateDto propertyDto)
    {
        var property = await _propertyService.GetByIdAsync(id);
        if (property is null) return NotFound();

        if(propertyDto.Image is not null)
        {
            string imagePath = await SaveImage(propertyDto.Image);
            property.ImageUrl = imagePath;
        }
        if(!string.IsNullOrEmpty(propertyDto.PropertyType))
            property.PropertyType = propertyDto.PropertyType;
        if (!string.IsNullOrEmpty(propertyDto.Location))
            property.Location = propertyDto.Location;
        if (propertyDto.Price > 0)
            property.Price = propertyDto.Price;
        if (!string.IsNullOrEmpty(propertyDto.Features))
            property.Features = propertyDto.Features;
        if (!string.IsNullOrEmpty(propertyDto.Description))
            property.Description = propertyDto.Description;
        if (!string.IsNullOrEmpty(propertyDto.BrokerContact))
            property.BrokerContact = propertyDto.BrokerContact;

        await _propertyService.UpdateAsync(property);
        return NoContent();
    }

    /// <summary>
    /// Deletes the property if exists: Only Broker can delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Broker")]
    public async Task<IActionResult> Delete(int id)
    {
        var property = await _propertyService.GetByIdAsync(id);
        if (property is null) return NotFound();

        await _propertyService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Retrieves a pagined list of properties, based on filter by location, price range, and so on
    /// </summary>
    /// <param name="location"></param>
    /// <param name="minPrice"></param>
    /// <param name="maxPrice"></param>
    /// <param name="propertyType"></param>
    /// <param name="brokerContact"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("search")]
    [Authorize(Roles = "Seeker,Broker")]
    public async Task<IActionResult> Search(
        [FromQuery] string? location,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] string? propertyType,
        [FromQuery] string? brokerContact,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
        )
    {
        var properties = await _propertyService.SearchAsync(location, minPrice, maxPrice, propertyType, brokerContact, pageNumber, pageSize);
        if (properties.Items.Any())
        {
            foreach (var property in properties.Items)
            {
                if (!string.IsNullOrEmpty(property.ImageUrl))
                {
                    var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{Path.GetFileName(property.ImageUrl)}";

                    // Set the full URL for ImageUrl before returning
                    property.ImageUrl = imageUrl;
                }
            }
        }
        return Ok(properties);
    }

    private async Task<string> SaveImage(IFormFile? image)
    {
        if(image is null || image.Length == 0)
            return string.Empty;

        string imagePath = Path.Combine(_environment.WebRootPath, "images");
        if (!Directory.Exists(imagePath))
            Directory.CreateDirectory(imagePath);

        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

        string filePath = Path.Combine(imagePath, uniqueFileName);

        using(var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        return $"/images/{uniqueFileName}";
    }
}
