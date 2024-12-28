using Microsoft.AspNetCore.Http;

namespace HouseBrokerApplication.Application.DTOs
{
    public class PropertyDto
    {
        public string PropertyType { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Features { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BrokerContact { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
