namespace CosmosDB.Entities;

public class DefaultAddress
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public DefaultAddress() { }

    public DefaultAddress(Address address)
    {
        Street = address.Street;
        City = address.City;
        State = address.State;
        ZipCode = address.ZipCode;
    }
}