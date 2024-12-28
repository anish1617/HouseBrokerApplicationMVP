using HouseBrokerApplication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseBrokerApplication.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // uncomment following region if you want to seed 1000 Properties datas to test pagination, filtering
        #region Seed1000PropertiesData
        //var propertyTypes = new[] { "Apartment", "Villa", "Cottage", "Bunglow", "Rent" };
        //var locations = new[] { "Bhatbhateni", "DilliBazar", "Putalisadak", "Baluwatar", "Naxal", "Hetauda", "Biratnagar", "Pokhara" };
        //var properties = new List<Property>();
        //var random = new Random();

        //for(int i = 10; i<= 1000; i++)
        //{
        //    properties.Add(new Property
        //    {
        //        Id = i,
        //        PropertyType = propertyTypes[random.Next(propertyTypes.Length)],
        //        Location = locations[random.Next(locations.Length)],
        //        Price = random.Next(50000, 10000000),
        //        Features = $"Feature {random.Next(1,10)}",
        //        Description = $"Description for property{i}",
        //        BrokerContact = $"broker{random.Next(1,10)}@example.com",
        //        ImageUrl = ""
        //    });
        //}

        //modelBuilder.Entity<Property>().HasData(properties);
        #endregion
    }
    public DbSet<Property> Properties { get; set; }

}
