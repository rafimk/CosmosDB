using CosmosDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace CosmosDB.Data;

public class DemoContext : DbContext
{
    public DemoContext(DbContextOptions<DemoContext> options) : base(options) 
    { 
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultContainer("Orders");

        builder.Entity<Customer>()
            .ToContainer(nameof(Customer))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

        builder.Entity<Order>()
            .ToContainer(nameof(Order))
            .HasPartitionKey(o => o.CustomerId)
            .HasNoDiscriminator();

        builder.Entity<Address>()
            .ToContainer(nameof(Address))
            .HasPartitionKey(c => c.CustomerId)
            .HasNoDiscriminator();
    }
}