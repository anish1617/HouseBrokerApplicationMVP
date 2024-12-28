using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseBrokerApplication.Domain.Entities;
/// <summary>
/// Represents a Property in a system.
/// </summary>
public class Property
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string PropertyType { get; set; } = string.Empty;
    [Required]
    public string Location { get; set; } = string.Empty;
    [Required]
    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public string Features { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string BrokerContact { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

}
