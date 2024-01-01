namespace CosmosDB.Entities;

public class Customer
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public DefaultAddress? DefaultBillingAddress { get; set; }
    public DefaultAddress? DefaultShippingAddress { get; set; }

    public ICollection<Order>? RecentOrders { get; set; }
}