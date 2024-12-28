using FluentAssertions;
using HouseBrokerApplication.Application.Interfaces;
using HouseBrokerApplication.Domain.Entities;
using NSubstitute;

namespace HouseBrokerApplication.Tests;

public class PropertyServiceTests
{
    private readonly IPropertyService _propertyRepository;

    public PropertyServiceTests()
    {
        _propertyRepository = Substitute.For<IPropertyService>();
    }

    [Fact]
    public async Task AddAsync_ShouldCallAddAsyncOnRepository()
    {
        // Arrange
        var property = new Property
        {
            PropertyType = "House",
            Description = "Testing House create",
            Price = 500000,
            Location = "Baneswor",
            BrokerContact = "broker1@example.com",
            Features = "Bed, Pool, Bathroom"
        };
        
        //Act
        await _propertyRepository.AddAsync(property);

        //Assert
        await _propertyRepository.Received(1).AddAsync(Arg.Is<Property>(p => p == property));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProperties()
    {
        // arrange
        var properties = new List<Property>
        {
            new Property
            {
                PropertyType = "Villa",
                Description = "Testing House create",
                Price = 6000000,
                Location = "Naxal",
                BrokerContact = "broker2@example.com",
                Features = "Bed, Pool, Bathroom"
            },
            new Property
            {
                PropertyType = "Cottage",
                Description = "Testing House create",
                Price = 8000000,
                Location = "Bhatbhateni",
                BrokerContact = "broker3@example.com",
                Features = "Bed, Pool, Bathroom"
            }
        };

        _propertyRepository.GetAllAsync().Returns(properties);

        // Act
        var result = await _propertyRepository.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.First().PropertyType.Should().Be("Villa");
        await _propertyRepository.Received(1).GetAllAsync();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProperty_WherePropertyExist()
    {
        // arrange
        var propertyId = 1;
        var expectedProperty = new Property
        {
            Id = propertyId,
            PropertyType = "House",
            Description = "Test Description",
            Price = 10000000,
            Location = "China company",
            BrokerContact = "broker@example.com",
            Features = "Bed, Bathroom, Toilet, Kitchen",
            ImageUrl = "/images/Demo.jpg"
        };

        _propertyRepository.GetByIdAsync(propertyId).Returns(expectedProperty);

        // act
        var result = await _propertyRepository.GetByIdAsync(propertyId);

        // assert
        result.Should().NotBeNull();
        result.Id.Should().Be(propertyId);
        result.PropertyType.Should().Be("House");
        result.Description.Should().Be("Test Description");
        result.Price.Should().Be(10000000);
        result.Location.Should().Be("China company");
        result.BrokerContact.Should().Be("broker@example.com");
        result.Features.Should().Be("Bed, Bathroom, Toilet, Kitchen");
        result.ImageUrl.Should().Be("/images/Demo.jpg");

        await _propertyRepository.Received(1).GetByIdAsync(propertyId);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProperty()
    {
        // arrange
        var propertyId = 5;
        _propertyRepository.DeleteAsync(propertyId).Returns(Task.CompletedTask);

        // act
        await _propertyRepository.DeleteAsync(propertyId);

        // assert
        await _propertyRepository.Received(1).DeleteAsync(Arg.Is<int>(id => id == propertyId));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProperty()
    {
        // arrange
        var property = new Property
        {
            Id = 5,
            PropertyType = "Updated Property Type",
            Description = "Updated Description",
            Price = 1000000000,
            Location = "Lalitpur",
            BrokerContact = "broker@example.com",
            Features = "Bed, Bathroom, Toilet, Kitchen"
        };

        _propertyRepository.UpdateAsync(Arg.Any<Property>()).Returns(Task.CompletedTask);

        // act
        await _propertyRepository.UpdateAsync(property);

        // assert
        await _propertyRepository.Received(1).UpdateAsync(Arg.Is<Property>(p => p.Id == property.Id && p.PropertyType == "Updated Property Type"));
    }
}
