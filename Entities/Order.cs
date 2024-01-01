namespace CosmosDB.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid TrackingNumber { get; set; }
    
    public DefaultAddress ShipToAddress { get; set; }
}